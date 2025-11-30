using Elektrijaam_ORM_11.Data;
using Elektrijaam_ORM_11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Elektrijaam_ORM_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArveController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArveController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("maksmata")]
        public async Task<ActionResult<IEnumerable<Arve>>> GetMaksmataArved()
        {
            return await _context.Arved
                .Where(a => !a.Maksestaatus.Makstud)
                .ToListAsync();
        }

        [HttpGet("tarbija/{tarbijaId}/maksmata")]
        public async Task<ActionResult<IEnumerable<Arve>>> GetMaksmataArvedTarbija(int tarbijaId)
        {
            return await _context.Arved
                .Where(a => a.TarbijaId == tarbijaId && !a.Maksestaatus.Makstud)
                .ToListAsync();
        }


        // GET: api/Arve
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Arve>>> GetArves()
        {
            return await _context.Arved.ToListAsync();
        }

        // GET: api/Arve/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Arve>> GetArve(int id)
        {
            var arve = await _context.Arved.FindAsync(id);

            if (arve == null)
            {
                return NotFound();
            }

            return arve;
        }

        // PUT: api/Arve/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArve(int id, Arve arve)
        {
            if (id != arve.Id)
            {
                return BadRequest();
            }

            _context.Entry(arve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArveExists(id))
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

        // POST: api/Arve
        [HttpPost]
        public async Task<ActionResult<Arve>> PostArve(Arve arve)
        {
            _context.Arved.Add(arve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArve", new { id = arve.Id }, arve);
        }

        // DELETE: api/Arve/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArve(int id)
        {
            var arve = await _context.Arved.FindAsync(id);
            if (arve == null)
            {
                return NotFound();
            }

            _context.Arved.Remove(arve);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArveExists(int id)
        {
            return _context.Arved.Any(e => e.Id == id);
        }
    }
}