using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareCircle.Data;
using ShareCircle.Models;

namespace ShareCircle.Controllers
{
    public class StrosekController : Controller
    {
        private readonly ShareCircleDbContext _context;

        public StrosekController(ShareCircleDbContext context)
        {
            _context = context;
        }

        // GET: Strosek
        public async Task<IActionResult> Index()
        {
            var shareCircleDbContext = _context.Strosek.Include(s => s.Placnik).Include(s => s.Skupina);
            return View(await shareCircleDbContext.ToListAsync());
        }

        // GET: Strosek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strosek = await _context.Strosek
                .Include(s => s.Placnik)
                .Include(s => s.Skupina)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (strosek == null)
            {
                return NotFound();
            }

            return View(strosek);
        }

        // GET: Strosek/Create
        public IActionResult Create()
        {
            ViewData["ID_placnika"] = new SelectList(_context.Uporabnik, "ID", "Ime");
            ViewData["ID_skupine"] = new SelectList(_context.Skupina, "ID", "ImeSkupine");
            return View();
        }

        // POST: Strosek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ID_placnika,ID_skupine,StevilkaStroska,CelotniZnesek,DatumPlacila")] Strosek strosek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(strosek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_placnika"] = new SelectList(_context.Uporabnik, "ID", "Ime", strosek.ID_placnika);
            ViewData["ID_skupine"] = new SelectList(_context.Skupina, "ID", "ImeSkupine", strosek.ID_skupine);
            return View(strosek);
        }

        // GET: Strosek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strosek = await _context.Strosek.FindAsync(id);
            if (strosek == null)
            {
                return NotFound();
            }
            ViewData["ID_placnika"] = new SelectList(_context.Uporabnik, "ID", "Ime", strosek.ID_placnika);
            ViewData["ID_skupine"] = new SelectList(_context.Skupina, "ID", "ImeSkupine", strosek.ID_skupine);
            return View(strosek);
        }

        // POST: Strosek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ID_placnika,ID_skupine,StevilkaStroska,CelotniZnesek,DatumPlacila")] Strosek strosek)
        {
            if (id != strosek.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(strosek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrosekExists(strosek.ID))
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
            ViewData["ID_placnika"] = new SelectList(_context.Uporabnik, "ID", "Ime", strosek.ID_placnika);
            ViewData["ID_skupine"] = new SelectList(_context.Skupina, "ID", "ImeSkupine", strosek.ID_skupine);
            return View(strosek);
        }

        // GET: Strosek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strosek = await _context.Strosek
                .Include(s => s.Placnik)
                .Include(s => s.Skupina)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (strosek == null)
            {
                return NotFound();
            }

            return View(strosek);
        }

        // POST: Strosek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var strosek = await _context.Strosek.FindAsync(id);
            if (strosek != null)
            {
                _context.Strosek.Remove(strosek);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrosekExists(int id)
        {
            return _context.Strosek.Any(e => e.ID == id);
        }
    }
}
