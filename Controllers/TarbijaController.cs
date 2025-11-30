using Elektrijaam_ORM_11.Data;
using Elektrijaam_ORM_11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Elektrijaam_ORM_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarbijaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TarbijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tarbija
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarbija>>> GetTarbijas()
        {
            return await _context.Tarbijad.ToListAsync();
        }

        // GET: api/Tarbija/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarbija>> GetTarbija(int id)
        {
            var tarbija = await _context.Tarbijad.FindAsync(id);

            if (tarbija == null)
            {
                return NotFound();
            }

            return tarbija;
        }

        // PUT: api/Tarbija/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarbija(int id, Tarbija tarbija)
        {
            if (id != tarbija.Id)
            {
                return BadRequest();
            }

            // Validate that the foreign keys exist
            var kontaktExists = await _context.Kontaktandmed.AnyAsync(k => k.Id == tarbija.KontaktandmedId);
            var asukohtExists = await _context.Asukohad.AnyAsync(a => a.Id == tarbija.AsukohtId);

            if (!kontaktExists)
            {
                return BadRequest($"Contact with ID {tarbija.KontaktandmedId} does not exist.");
            }

            if (!asukohtExists)
            {
                return BadRequest($"Location with ID {tarbija.AsukohtId} does not exist.");
            }

            _context.Entry(tarbija).State = EntityState.Modified; try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarbijaExists(id))
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

        // POST: api/Tarbija
        [HttpPost]
        public async Task<ActionResult<Tarbija>> PostTarbija(Tarbija tarbija)
        {
            // Validate that the foreign keys exist
            var kontaktExists = await _context.Kontaktandmed.AnyAsync(k => k.Id == tarbija.KontaktandmedId);
            var asukohtExists = await _context.Asukohad.AnyAsync(a => a.Id == tarbija.AsukohtId);

            if (!kontaktExists)
            {
                return BadRequest($"Contact with ID {tarbija.KontaktandmedId} does not exist.");
            }

            if (!asukohtExists)
            {
                return BadRequest($"Location with ID {tarbija.AsukohtId} does not exist.");
            }

            _context.Tarbijad.Add(tarbija);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarbija", new { id = tarbija.Id }, tarbija);
        }

        // DELETE: api/Tarbija/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarbija(int id)
        {
            var tarbija = await _context.Tarbijad.FindAsync(id);
            if (tarbija == null)
            {
                return NotFound();
            }

            _context.Tarbijad.Remove(tarbija);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarbijaExists(int id)
        {
            return _context.Tarbijad.Any(e => e.Id == id);
        }
    }
}
