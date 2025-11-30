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
    public class MaksestaatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaksestaatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Maksestaatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maksestaatus>>> GetMaksestaatused()
        {
            return await _context.Maksestaatused.ToListAsync();
        }

        // GET: api/Maksestaatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Maksestaatus>> GetMaksestaatus(int id)
        {
            var maksestaatus = await _context.Maksestaatused.FindAsync(id);

            if (maksestaatus == null)
            {
                return NotFound();
            }

            return maksestaatus;
        }

        // PUT: api/Maksestaatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaksestaatus(int id, Maksestaatus maksestaatus)
        {
            if (id != maksestaatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(maksestaatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaksestaatusExists(id))
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

        // POST: api/Maksestaatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Maksestaatus>> PostMaksestaatus(Maksestaatus maksestaatus)
        {
            _context.Maksestaatused.Add(maksestaatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaksestaatus", new { id = maksestaatus.Id }, maksestaatus);
        }

        // DELETE: api/Maksestaatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaksestaatus(int id)
        {
            var maksestaatus = await _context.Maksestaatused.FindAsync(id);
            if (maksestaatus == null)
            {
                return NotFound();
            }

            _context.Maksestaatused.Remove(maksestaatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool MaksestaatusExists(int id)
        {
            return _context.Maksestaatused.Any(e => e.Id == id);
        }
    }
}
