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
    public class ClanSkupineController : Controller
    {
        private readonly ShareCircleDbContext _context;

        public ClanSkupineController(ShareCircleDbContext context)
        {
            _context = context;
        }

        // GET: ClanSkupine
        public async Task<IActionResult> Index()
        {
            var shareCircleDbContext = _context.ClanSkupine.Include(c => c.Skupina).Include(c => c.Uporabnik);
            return View(await shareCircleDbContext.ToListAsync());
        }

        // GET: ClanSkupine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanSkupine = await _context.ClanSkupine
                .Include(c => c.Skupina)
                .Include(c => c.Uporabnik)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clanSkupine == null)
            {
                return NotFound();
            }

            return View(clanSkupine);
        }

        // GET: ClanSkupine/Create
        public IActionResult Create()
        {
            ViewData["SkupinaID"] = new SelectList(_context.Skupina, "ID", "ImeSkupine");
            ViewData["UporabnikID"] = new SelectList(_context.Uporabnik, "Id", "UserName");
            return View();
        }

        // POST: ClanSkupine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UporabnikID,SkupinaID,DatumPridruzitve")] ClanSkupine clanSkupine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clanSkupine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkupinaID"] = new SelectList(_context.Skupina, "ID", "ImeSkupine", clanSkupine.SkupinaID);
            ViewData["UporabnikID"] = new SelectList(_context.Uporabnik, "Id", "UserName", clanSkupine.UporabnikID);
            return View(clanSkupine);
        }

        // GET: ClanSkupine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanSkupine = await _context.ClanSkupine.FindAsync(id);
            if (clanSkupine == null)
            {
                return NotFound();
            }
            ViewData["SkupinaID"] = new SelectList(_context.Skupina, "ID", "ID", clanSkupine.SkupinaID);
            ViewData["UporabnikID"] = new SelectList(_context.Uporabnik, "Id", "UserName", clanSkupine.UporabnikID);
            return View(clanSkupine);
        }

        // POST: ClanSkupine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UporabnikID,SkupinaID,DatumPridruzitve")] ClanSkupine clanSkupine)
        {
            if (id != clanSkupine.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clanSkupine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClanSkupineExists(clanSkupine.ID))
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
            ViewData["SkupinaID"] = new SelectList(_context.Skupina, "ID", "ID", clanSkupine.SkupinaID);
            ViewData["UporabnikID"] = new SelectList(_context.Uporabnik, "Id", "UserName", clanSkupine.UporabnikID);
            return View(clanSkupine);
        }

        // GET: ClanSkupine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanSkupine = await _context.ClanSkupine
                .Include(c => c.Skupina)
                .Include(c => c.Uporabnik)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clanSkupine == null)
            {
                return NotFound();
            }

            return View(clanSkupine);
        }

        // POST: ClanSkupine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clanSkupine = await _context.ClanSkupine.FindAsync(id);
            if (clanSkupine != null)
            {
                _context.ClanSkupine.Remove(clanSkupine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClanSkupineExists(int id)
        {
            return _context.ClanSkupine.Any(e => e.ID == id);
        }
    }
}
