using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareCircle.Data;
using ShareCircle.Models;
using Microsoft.AspNetCore.Authorization;

namespace ShareCircle.Controllers
{
    public class VraciloController : Controller
    {
        private readonly ShareCircleDbContext _context;

        public VraciloController(ShareCircleDbContext context)
        {
            _context = context;
        }

        // GET: Vracilo
        public async Task<IActionResult> Index()
        {
            var shareCircleDbContext = _context.Vracilo.Include(v => v.Dolžnik).Include(v => v.Skupina);
            return View(await shareCircleDbContext.ToListAsync());
        }

        // GET: Vracilo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vracilo = await _context.Vracilo
                .Include(v => v.Dolžnik)
                .Include(v => v.Skupina)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vracilo == null)
            {
                return NotFound();
            }

            return View(vracilo);
        }

        // GET: Vracilo/Create
        public IActionResult Create()
        {
            ViewData["ID_dolznika"] = new SelectList(_context.Uporabnik, "ID", "Ime");
            ViewData["ID_skupine"] = new SelectList(_context.Skupina, "ID", "ImeSkupine");
            return View();
        }

        // POST: Vracilo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ID_dolznika,ID_skupine,StevilkaVracila,ZnesekVracila,DatumVracila")] Vracilo vracilo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vracilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_dolznika"] = new SelectList(_context.Uporabnik, "ID", "Ime", vracilo.ID_dolznika);
            ViewData["ID_skupine"] = new SelectList(_context.Skupina, "ID", "ImeSkupine", vracilo.ID_skupine);
            return View(vracilo);
        }

        // GET: Vracilo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vracilo = await _context.Vracilo.FindAsync(id);
            if (vracilo == null)
            {
                return NotFound();
            }
            ViewData["ID_dolznika"] = new SelectList(_context.Uporabnik, "ID", "Ime", vracilo.ID_dolznika);
            ViewData["ID_skupine"] = new SelectList(_context.Skupina, "ID", "ImeSkupine", vracilo.ID_skupine);
            return View(vracilo);
        }

        // POST: Vracilo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ID_dolznika,ID_skupine,StevilkaVracila,ZnesekVracila,DatumVracila")] Vracilo vracilo)
        {
            if (id != vracilo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vracilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VraciloExists(vracilo.ID))
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
            ViewData["ID_dolznika"] = new SelectList(_context.Uporabnik, "ID", "Ime", vracilo.ID_dolznika);
            ViewData["ID_skupine"] = new SelectList(_context.Skupina, "ID", "ImeSkupine", vracilo.ID_skupine);
            return View(vracilo);
        }

        // GET: Vracilo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vracilo = await _context.Vracilo
                .Include(v => v.Dolžnik)
                .Include(v => v.Skupina)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vracilo == null)
            {
                return NotFound();
            }

            return View(vracilo);
        }

        // POST: Vracilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vracilo = await _context.Vracilo.FindAsync(id);
            if (vracilo != null)
            {
                _context.Vracilo.Remove(vracilo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VraciloExists(int id)
        {
            return _context.Vracilo.Any(e => e.ID == id);
        }
    }
}
