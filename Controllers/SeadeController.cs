using Elektrijaam_ORM_11.Data;
using Elektrijaam_ORM_11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Elektrijaam_ORM_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeadeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeadeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Seadmed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seade>>> GetSeades()
        {
            return await _context.Seadmed.ToListAsync();
        }

        // GET: api/Seaded/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seade>> GetSeade(int id)
        {
            var seade = await _context.Seadmed.FindAsync(id);

            if (seade == null)
            {
                return NotFound();
            }

            return seade;
        }

        // PUT: api/Seaded/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeade(int id, Seade seade)
        {
            if (id != seade.Id)
            {
                return BadRequest();
            }

            if (seade.JargmineHooldusAeg < DateTime.Now)
            {
                return BadRequest("Hoodlusaeg ei saa olla minevikus");
            }

            if (seade.JaakMaksumus > seade.SoetusMaksumus)
            {
                return BadRequest("Jääkmaksumus ei tohi olla suurem kui seotusmaksumus");
            }

            _context.Entry(seade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeadeExists(id))
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

        // POST: api/Seaded
        [HttpPost]
        public async Task<ActionResult<Seade>> PostSeade(Seade seade)
        {
            if (seade.JargmineHooldusAeg < DateTime.Now)
            {
                return BadRequest("Hoodlusaeg ei saa olla minevikus");
            }

            if (seade.JaakMaksumus > seade.SoetusMaksumus)
            {
                return BadRequest("Jääkmaksumus ei tohi olla suurem kui seotusmaksumus");
            }

            _context.Seadmed.Add(seade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeade", new { id = seade.Id }, seade);
        }

        // DELETE: api/Seaded/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeade(int id)
        {
            var seade = await _context.Seadmed.FindAsync(id);
            if (seade == null)
            {
                return NotFound();
            }

            _context.Seadmed.Remove(seade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeadeExists(int id)
        {
            return _context.Seadmed.Any(e => e.Id == id);
        }
    }
}