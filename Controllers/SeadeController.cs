using Elektrijaam_ORM_11.Data;
using Elektrijaam_ORM_11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elektrijaam_ORM_11.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeadeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeadeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /seade
        [HttpGet]
        public IActionResult GetSeadmed()
        {
            var seadmed = _context.Seadmed.ToList();
            return Ok(seadmed);
        }

        // POST: /seade (Lisamine)
        [HttpPost]
        public IActionResult AddSead([FromBody] Seade seade)
        {
            if (seade.JargmineHooldusAeg < DateTime.Today)
            {
                return BadRequest("Hooldusaeg ei tohi olla minevikus");
            }
            if (seade.JaakMaksumus < seade.SoetusMaksumus)
            {
                return BadRequest("Jaakmaksmus ei tohi olla suurem kui soetusmaksmus");
            }

            _context.Seadmed.Add(seade);
            _context.SaveChanges();
            return Ok();
        }

        // PUT: seade/id (Muutmine)
        [HttpPut("{id}")]
        public IActionResult UpdateSeade(int id, [FromBody] Seade seade)
        {
            var existing = _context.Seadmed.Find(id);
            if (existing == null)
                return NotFound();

            if (seade.JargmineHooldusAeg < DateTime.Today)
                return BadRequest("Hooldusaeg ei tohi olla minevikus.");

            if (seade.JaakMaksumus > seade.SoetusMaksumus)
                return BadRequest("Jääkmaksumus ei tohi olla suurem kui soetusmaksumus.");

            existing.Nimetus = seade.Nimetus;
            existing.Tootja = seade.Tootja;
            existing.JargmineHooldusAeg = seade.JargmineHooldusAeg;
            existing.JaakMaksumus = seade.JaakMaksumus;
            existing.SoetusMaksumus = seade.SoetusMaksumus;
            existing.Aktiivne = seade.Aktiivne;

            _context.SaveChanges();
            return Ok(existing);
        }
    }
}
