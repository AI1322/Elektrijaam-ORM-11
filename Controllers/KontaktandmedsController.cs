using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elektrijaam_ORM_11.Data;
using Elektrijaam_ORM_11.Models;

namespace Elektrijaam_ORM_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KontaktandmedsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KontaktandmedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Kontaktandmeds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kontaktandmed>>> GetKontaktandmed()
        {
            return await _context.Kontaktandmed.ToListAsync();
        }

        // GET: api/Kontaktandmeds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kontaktandmed>> GetKontaktandmed(int id)
        {
            var kontaktandmed = await _context.Kontaktandmed.FindAsync(id);

            if (kontaktandmed == null)
            {
                return NotFound();
            }

            return kontaktandmed;
        }

        // PUT: api/Kontaktandmeds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKontaktandmed(int id, Kontaktandmed kontaktandmed)
        {
            if (id != kontaktandmed.Id)
            {
                return BadRequest();
            }

            _context.Entry(kontaktandmed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KontaktandmedExists(id))
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

        // POST: api/Kontaktandmeds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kontaktandmed>> PostKontaktandmed(Kontaktandmed kontaktandmed)
        {
            _context.Kontaktandmed.Add(kontaktandmed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKontaktandmed", new { id = kontaktandmed.Id }, kontaktandmed);
        }

        // DELETE: api/Kontaktandmeds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKontaktandmed(int id)
        {
            var kontaktandmed = await _context.Kontaktandmed.FindAsync(id);
            if (kontaktandmed == null)
            {
                return NotFound();
            }

            _context.Kontaktandmed.Remove(kontaktandmed);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KontaktandmedExists(int id)
        {
            return _context.Kontaktandmed.Any(e => e.Id == id);
        }
    }
}
