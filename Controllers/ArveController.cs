using Elektrijaam_ORM_11.Data;
using Elektrijaam_ORM_11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Elektrijaam_ORM_11.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArveController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArveController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("tarbija/{id}/arved")]
        public IActionResult GetTarbijaArved(int id)
        {
            try
            {
                var tarbija = _context.Tarbijad
                    .Include(t => t.Arved)
                    .FirstOrDefault(t => t.Id == id);

                if (tarbija == null)
                    return BadRequest("Tarbija ei leitud");

                return Ok(tarbija.Arved);
            }
            catch (Exception)
            {
                return BadRequest("Tarbija ei leitud");
            }
        }

        [HttpGet("tarbija/{id}/arved/summa")]
        public IActionResult GetTarbijaArveteSumma(int id)
        {
            try
            {
                var tarbija = _context.Tarbijad
                    .Include(t => t.Arved)
                    .FirstOrDefault(t => t.Id == id);

                if (tarbija == null)
                    return BadRequest("Tarbija ei leitud");

                decimal summa = 0;
                foreach (var arve in tarbija.Arved)
                {
                    summa += arve.Summa;
                }

                return Ok(new { TarbijaId = id, KokkuSumma = summa });
            }
            catch (Exception)
            {
                return BadRequest("Tarbija ei leitud");
            }
        }


        [HttpGet("maksmata")]
        public IActionResult GetMaksmataArved()
        {
            var arved = _context.Arved.ToList();
            List<Arve> maksmataArved = new List<Arve>();
            foreach (Arve arve in arved)
            {
                if (!arve.Maksestaatus.Makstud)
                {
                    maksmataArved.Add(arve);
                }
            }
            if (maksmataArved != null)
            {
                return Ok(arved);
            }
            else
            {
                return Ok("Kõik arved on makstud");
            }
        }

        [HttpGet("maksmata/uletatud")]
        public IActionResult GetUletatudArved()
        {
            List<Arve> uletatudArved = new List<Arve>();
            foreach (Arve arve in _context.Arved.ToList())
            {
                if (arve.Maksestaatus.MaksmiseTahtaeg < DateTime.Today && !arve.Maksestaatus.Makstud)
                {
                    uletatudArved.Add(arve);
                }
            }
            if (uletatudArved.IsNullOrEmpty())
            {
                return Ok("Kõik arved on makstud");
            }
            else
            {
                return Ok(uletatudArved);
            }
        }

        [HttpGet("tarbija/{id}/maksmata")]
        public IActionResult GetTarbijaMaksmataArved(int id)
        {
            try
            {
                Tarbija tarbija = _context.Tarbijad.Find(id);
                List<Arve> maksmataArved = new List<Arve>();
                foreach (Arve arve in tarbija.Arved.ToList())
                {
                    if (!arve.Maksestaatus.Makstud)
                    {
                        maksmataArved.Add(arve);
                    }
                }
                if (maksmataArved.IsNullOrEmpty())
                {
                    return Ok("Kõik arved on makstud");
                }
                return Ok(maksmataArved);
            }
            catch (Exception ex)
            {
                return BadRequest("Tarbija ei leidnud");
            }
        }

        [HttpGet("tarbija/{id}/maksmata/uletatud")]
        public IActionResult GetTarbijaUletatudArved(int id)
        {
            try
            {
                Tarbija tarbija = _context.Tarbijad.Find(id);
                List<Arve> uletatudArved = new List<Arve>();

                foreach (Arve arve in tarbija.Arved.ToList())
                {
                    if (arve.Maksestaatus.MaksmiseTahtaeg < DateTime.Today && !arve.Maksestaatus.Makstud)
                    {
                        uletatudArved.Add(arve);
                    }
                }
                if (uletatudArved.IsNullOrEmpty())
                {
                    return Ok("Kõik arved on makstud");
                }
                return Ok(uletatudArved);
            }
            catch (Exception ex)
            {
                return BadRequest("Tarbija ei leidnud");
            }
        }


    }
}
