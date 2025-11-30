using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Elektrijaam_ORM_11.Data;
using Elektrijaam_ORM_11.Models;

namespace Elektrijaam_ORM_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsukohtController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AsukohtController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Asukoht
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asukoht>>> GetAsukohas()
        {
            return await _context.Asukohad.ToListAsync();
        }

        // GET: api/Asukoht/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asukoht>> GetAsukoht(int id)
        {
            var asukoht = await _context.Asukohad.FindAsync(id);

            if (asukoht == null)
            {
                return NotFound();
            }

            return asukoht;
        }

        // PUT: api/Asukoht/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsukoht(int id, Asukoht asukoht)
        {
            if (id != asukoht.Id)
            {
                return BadRequest();
            }

            _context.Entry(asukoht).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsukohtExists(id))
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

        // POST: api/Asukoht
        [HttpPost]
        public async Task<ActionResult<Asukoht>> PostAsukoht(Asukoht asukoht)
        {
            _context.Asukohad.Add(asukoht);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsukoht", new { id = asukoht.Id }, asukoht);
        }

        // DELETE: api/Asukoht/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsukoht(int id)
        {
            var asukoht = await _context.Asukohad.FindAsync(id);
            if (asukoht == null)
            {
                return NotFound();
            }

            _context.Asukohad.Remove(asukoht);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsukohtExists(int id)
        {
            return _context.Asukohad.Any(e => e.Id == id);
        }
    }
}