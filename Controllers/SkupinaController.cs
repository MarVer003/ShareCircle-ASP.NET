using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareCircle.Data;
using ShareCircle.Models;

namespace ShareCircle.Controllers
{
    [Authorize]
    public class SkupinaController : Controller
    {
        private readonly ShareCircleDbContext _context;

        public SkupinaController(ShareCircleDbContext context)
        {
            _context = context;
        }

        // GET: Skupina
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skupina.ToListAsync());
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

            ViewData["Uporabniki"] = new SelectList(items, "Uporabnik.ID", "FullName");

            return View(new Strosek { ID_skupine = skupinaId });
        }

        // Action to handle the form submission for adding an expense
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
                await RecalculateBalancesAsync(strosek.ID_skupine);

                return RedirectToAction("Details", new { id = strosek.ID_skupine });
            }

            return View(strosek); // Return view with validation errors
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

            ViewData["Uporabniki"] = new SelectList(items, "Uporabnik.ID", "FullName");

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
                await RecalculateBalancesAsync(vracilo.ID_skupine);


                return RedirectToAction("Details", new { id = vracilo.ID_skupine });
            }

            return View(vracilo); // Return view with validation errors
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStrosek(int id, int skupinaId)
        {
            try
            {
                // Retrieve the expense to delete
                var strosek = await _context.Strosek
                    .Include(s => s.RazdelitveStroskov)
                    .FirstOrDefaultAsync(s => s.ID == id);

                if (strosek == null)
                {
                    return NotFound();
                }

                // Tudi razdelitve je potrebno izbrisati
                if (strosek.RazdelitveStroskov != null)
                {
                    _context.RazdelitevStroska.RemoveRange(strosek.RazdelitveStroskov);
                }

                // Izbriši strošek
                _context.Strosek.Remove(strosek);
                _context.SaveChanges();
                await RecalculateBalancesAsync(strosek.ID_skupine);

                // Vrni se nazaj na glavno stran skupine
                return RedirectToAction("Details", new { id = skupinaId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home"); // Adjust the error handling as needed
            }
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
                await RecalculateBalancesAsync(vracilo.ID_skupine);

                // Vrni se nazaj na glavno stran skupine
                return RedirectToAction("Details", new { id = skupinaId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home"); // Adjust the error handling as needed
            }
        }

        private async Task RecalculateBalancesAsync(int SkupinaId)
        {
            var claniSkupine = await _context.ClanSkupine
                .Where(cs => cs.SkupinaID == SkupinaId)
                .ToArrayAsync();

            var stroskiInRazdelitve = await _context.Strosek
                .Include(s => s.RazdelitveStroskov)
                .Where(cs => cs.ID_skupine == SkupinaId)
                .ToArrayAsync();

            var vracila = await _context.Vracilo
                .Where(s => s.ID_skupine == SkupinaId)
                .ToArrayAsync();

            Dictionary<int, decimal> claniZneski = [];
            foreach (var clan in claniSkupine)
            {
                claniZneski.Add(clan.UporabnikID, 0);
            }
            foreach (var strosek in stroskiInRazdelitve)
            {
                claniZneski[strosek.ID_placnika] += strosek.CelotniZnesek;
                foreach (var razdelitev in strosek.RazdelitveStroskov)
                {
                    claniZneski[razdelitev.ID_dolznika] -= razdelitev.Znesek;
                }
            }
            foreach (var vracilo in vracila)
            {
                claniZneski[vracilo.ID_dolznika] += vracilo.ZnesekVracila;
                claniZneski[vracilo.ID_upnika] -= vracilo.ZnesekVracila;
            }

            foreach (var clan in claniSkupine)
            {
                clan.Stanje = claniZneski[clan.UporabnikID];
                _context.Update(clan);
            }
            await _context.SaveChangesAsync();
        }




        // GET: Skupina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
