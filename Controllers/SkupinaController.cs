using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareCircle.Data;
using ShareCircle.Models;
using ShareCircle.Services;

namespace ShareCircle.Controllers
{
    [Authorize]
    public class SkupinaController : Controller
    {
        private readonly ShareCircleDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBalanceService _balanceService;

        public SkupinaController(ShareCircleDbContext context, UserManager<ApplicationUser> userManager, IBalanceService balanceService)
        {
            _context = context;
            _userManager = userManager;
            _balanceService = balanceService;
        }

        // GET: Skupina
        public async Task<IActionResult> Index()
        {
            var claniSkupin = await _context.ClanSkupine
                .Where(cs => cs.UporabnikID == _userManager.GetUserId(User))
                .Select(cs => cs.SkupinaID)
                .ToListAsync();

            var skupine = await _context.Skupina
                .Where(s => claniSkupin.Contains(s.ID))
                .ToListAsync();

            return View(skupine);
        }

        // GET: Skupina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _balanceService.RecalculateBalancesAsync(id);

            var skupina = await _context.Skupina
                .Include(s => s.Stroski)
                .ThenInclude(s => s.RazdelitveStroskov)
                .Include(s => s.Vracila)
                .ThenInclude(v => v.Dolžnik)
                .Include(s => s.Vracila)
                .ThenInclude(v => v.Upnik)
                .Include(s => s.ClanSkupine)
                .ThenInclude(cs => cs.Uporabnik)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (skupina == null)
            {
                return NotFound();
            }

            return View(skupina);
        }

        // GET: Skupina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skupina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ImeSkupine,DatumNastanka")] Skupina skupina)
        {
            if (ModelState.IsValid)
            {
                skupina.DatumNastanka = DateTime.Now;
                _context.Add(skupina);
                await _context.SaveChangesAsync();
                var user = _userManager.GetUserId(User);
                if (user != null)
                {
                    _context.Add(new ClanSkupine { UporabnikID = user, SkupinaID = skupina.ID });
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(skupina);
        }

        // GET: Skupina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skupina = await _context.Skupina.FindAsync(id);
            if (skupina == null)
            {
                return NotFound();
            }
            return View(skupina);
        }

        // POST: Skupina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ImeSkupine")] Skupina skupina)
        {
            if (id != skupina.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var obstojecaSkupina = await _context.Skupina.FindAsync(id);
                    if (obstojecaSkupina == null)
                    {
                        return NotFound();
                    }

                    obstojecaSkupina.ImeSkupine = skupina.ImeSkupine;

                    _context.Update(obstojecaSkupina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkupinaExists(skupina.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(skupina);
        }

        // GET: Skupina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skupina = await _context.Skupina
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skupina == null)
            {
                return NotFound();
            }

            return View(skupina);
        }

        // POST: Skupina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skupina = await _context.Skupina.FindAsync(id);
            if (skupina != null)
            {
                _context.Skupina.Remove(skupina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkupinaExists(int id)
        {
            return _context.Skupina.Any(e => e.ID == id);
        }
    }
}
