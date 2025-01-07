using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShareCircle.Models;
using Microsoft.AspNetCore.Authorization;
using ShareCircle.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShareCircle.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShareCircle.Controllers;

[Authorize]
public class VraciloController : Controller
{
    private readonly ShareCircleDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBalanceService _balanceService;

    public VraciloController(ShareCircleDbContext context, UserManager<ApplicationUser> userManager, IBalanceService balanceService)
    {
        _context = context;
        _userManager = userManager;
        _balanceService = balanceService;

    }

    // GET: Skupina/Details/PodrobnostiVracila/{id_skupine}?vraciloId={id_vracila}
    public async Task<IActionResult> PodrobnostiVracila(int id, int vraciloId)
    {
        var vracilo = await _context.Vracilo
            .Include(v => v.Dolžnik)
            .Include(v => v.Upnik)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ID == vraciloId);

        if (vracilo == null)
        {
            return NotFound();
        }

        return View(vracilo);
    }

    // Action to display the form for adding a back-payment
    [HttpGet]
    public IActionResult DodajVracilo(int skupinaId)
    {
        // Fetch necessary data (e.g., users in the group)
        var group = _context.Skupina
            .Include(s => s.ClanSkupine)  // Assuming ClanSkupine represents members
            .FirstOrDefault(s => s.ID == skupinaId);

        if (group == null) return NotFound();

        var items = _context.ClanSkupine
            .Include(cs => cs.Uporabnik)
            .Where(cs => cs.SkupinaID == skupinaId)
            .Select(cs => new
            {
                cs.UporabnikID,
                cs.Uporabnik,
                FullName = cs.Uporabnik.Ime + " " + cs.Uporabnik.Priimek
            })
            .ToList();

        ViewData["Uporabniki"] = new SelectList(items, "Uporabnik.Id", "Uporabnik.UserName");

        return View(new Vracilo { ID_skupine = skupinaId });
    }

    // Action to handle the form submission for adding a back-payment
    [HttpPost]
    public async Task<IActionResult> DodajVracilo([Bind("ID,ID_dolznika,ID_upnika,ID_skupine,ZnesekVracila,DatumPlacila")] Vracilo vracilo)
    {
        if (ModelState.IsValid)
        {
            vracilo.DatumVracila = DateTime.Now;
            _context.Vracilo.Add(vracilo);
            await _context.SaveChangesAsync();

            var dolznik = _context.ClanSkupine
                .First(cs => cs.UporabnikID == vracilo.ID_dolznika);

            var upnik = _context.ClanSkupine
                .First(cs => cs.UporabnikID == vracilo.ID_upnika);

            await _context.SaveChangesAsync();
            await _balanceService.RecalculateBalancesAsync(vracilo.ID_skupine);


            return RedirectToAction("Details", "Skupina", new { id = vracilo.ID_skupine });
        }

        return View(vracilo); // Return view with validation errors
    }

    [HttpPost]
    public async Task<IActionResult> DeleteVracilo(int id, int skupinaId)
    {
        try
        {
            // Retrieve the expense to delete
            var vracilo = await _context.Vracilo
                .FirstOrDefaultAsync(s => s.ID == id);

            if (vracilo == null)
            {
                return NotFound();
            }

            // Izbriši strošek
            _context.Vracilo.Remove(vracilo);
            _context.SaveChanges();
            await _balanceService.RecalculateBalancesAsync(vracilo.ID_skupine);

            // Vrni se nazaj na glavno stran skupine
            return RedirectToAction("Details", "Skupina", new { id = skupinaId });
        }
        catch (Exception ex)
        {
            return RedirectToAction("Error", "Home"); // Adjust the error handling as needed
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
