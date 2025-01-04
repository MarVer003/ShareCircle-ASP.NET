using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

                var placnik = _context.ClanSkupine
                .Where(cs => cs.UporabnikID == strosek.ID_placnika)
                .ToList();

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

                placnik[0].Stanje += strosek.CelotniZnesek;
                _context.ClanSkupine.Update(placnik[0]);

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
                    dolzniki[i].cs.Stanje -= dolgovaniZneski[i];
                    _context.ClanSkupine.Update(dolzniki[i].cs);
                    _context.RazdelitevStroska.Add(new() { ID_stroska = strosek.ID, ID_dolznika = dolzniki[i].UporabnikID, Znesek = dolgovaniZneski[i], Strosek = strosek, Dolznik = dolzniki[i].Uporabnik });
                }

                await _context.SaveChangesAsync();

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

                dolznik.Stanje += vracilo.ZnesekVracila;
                _context.ClanSkupine.Update(dolznik);

                upnik.Stanje -= vracilo.ZnesekVracila;
                _context.ClanSkupine.Update(upnik);

                await _context.SaveChangesAsync();


                return RedirectToAction("Details", new { id = vracilo.ID_skupine });
            }

            return View(vracilo); // Return view with validation errors
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
