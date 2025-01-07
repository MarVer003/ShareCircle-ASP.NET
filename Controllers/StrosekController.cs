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
public class StrosekController : Controller
{
    private readonly ShareCircleDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBalanceService _balanceService;

    public StrosekController(ShareCircleDbContext context, UserManager<ApplicationUser> userManager, IBalanceService balanceService)
    {
        _context = context;
        _userManager = userManager;
        _balanceService = balanceService;

    }

    // GET: Skupina/Details/PodrobnostiStroska/{id_skupine}?strosekId={id_stroska}
    public async Task<IActionResult> PodrobnostiStroska(int id, int strosekId)
    {
        var strosek = await _context.Strosek
            .Include(s => s.RazdelitveStroskov)
            .ThenInclude(rs => rs.Dolznik)
            .Include(s => s.Placnik)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ID == strosekId);

        if (strosek == null)
        {
            return NotFound();
        }

        return View(strosek);
    }

    // Action to display the form for adding an expense
    [HttpGet]
    public IActionResult DodajStrosek(int skupinaId)
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

        return View(new Strosek { ID_skupine = skupinaId });
    }

    [HttpPost]
    public async Task<IActionResult> DodajStrosek([Bind("ID,ID_placnika,ID_skupine,Naslov,CelotniZnesek,DatumPlacila")] Strosek strosek)
    {
        if (ModelState.IsValid)
        {
            strosek.DatumPlacila = DateTime.Now;
            _context.Strosek.Add(strosek);

            var dolzniki = _context.ClanSkupine
            .Include(cs => cs.Uporabnik)
            .Where(cs => cs.SkupinaID == strosek.ID_skupine && cs.UporabnikID != strosek.ID_placnika)
            .Select(cs => new
            {
                cs.UporabnikID,
                cs.Uporabnik,
                cs
            })
            .ToList();

            decimal znesek = strosek.CelotniZnesek;
            decimal dolgovanZnesek = Math.Floor(znesek / dolzniki.Count * 100) / 100;
            decimal skupniZnesek = dolgovanZnesek * dolzniki.Count;
            decimal ostanek = znesek - skupniZnesek;

            List<decimal> dolgovaniZneski = Enumerable.Repeat(dolgovanZnesek, dolzniki.Count).ToList();
            for (int i = 0; i < ostanek * 100; i++)
            {
                dolgovaniZneski[i] += 0.01m;
            }

            for (int i = 0; i < dolzniki.Count; i++)
            {
                _context.RazdelitevStroska.Add(new() { ID_stroska = strosek.ID, ID_dolznika = dolzniki[i].UporabnikID, Znesek = dolgovaniZneski[i], Strosek = strosek, Dolznik = dolzniki[i].Uporabnik });
            }

            await _context.SaveChangesAsync();
            await _balanceService.RecalculateBalancesAsync(strosek.ID_skupine);

            return RedirectToAction("Details", "Skupina", new { id = strosek.ID_skupine });
        }

        return View(strosek); // Return view with validation errors
    }

    [HttpPost]
    public async Task<IActionResult> DeleteStrosek(int id, int skupinaId)
    {
        try
        {
            // Retrieve the expense to delete
            var strosek = await _context.Strosek
                .FirstOrDefaultAsync(s => s.ID == id);

            if (strosek == null)
            {
                return NotFound();
            }

            // Izbriši strošek
            _context.Strosek.Remove(strosek);
            _context.SaveChanges();
            await _balanceService.RecalculateBalancesAsync(strosek.ID_skupine);

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
