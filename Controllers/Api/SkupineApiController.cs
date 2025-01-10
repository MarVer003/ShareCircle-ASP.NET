using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareCircle.Data;
using ShareCircle.Models;

namespace ShareCircle.Controllers_Api
{
    [Route("api/v1/Skupina")]
    [ApiController]
    public class SkupineApiController : ControllerBase
    {
        private readonly ShareCircleDbContext _context;

        public SkupineApiController(ShareCircleDbContext context)
        {
            _context = context;
        }

        // GET: api/SkupineApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skupina>>> GetSkupina()
        {
            return await _context.Skupina.ToListAsync();
        }

        // GET: api/SkupineApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Skupina>> GetSkupina(int id)
        {
            var skupina = await _context.Skupina.FindAsync(id);

            if (skupina == null)
            {
                return NotFound();
            }

            return skupina;
        }

        // PUT: api/SkupineApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkupina(int id, Skupina skupina)
        {
            if (id != skupina.ID)
            {
                return BadRequest();
            }

            _context.Entry(skupina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkupinaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SkupineApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Skupina>> PostSkupina(Skupina skupina)
        {
            _context.Skupina.Add(skupina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkupina", new { id = skupina.ID }, skupina);
        }

        // DELETE: api/SkupineApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkupina(int id)
        {
            var skupina = await _context.Skupina.FindAsync(id);
            if (skupina == null)
            {
                return NotFound();
            }

            _context.Skupina.Remove(skupina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkupinaExists(int id)
        {
            return _context.Skupina.Any(e => e.ID == id);
        }
    }
}
