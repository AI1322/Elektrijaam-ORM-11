using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elektrijaam_ORM_11.Models
{
    public class Seade
    {
        public int Id { get; set; }
        public string Nimetus { get; set; } = string.Empty;
        public string? Tootja { get; set; } = string.Empty;
        public DateTime JargmineHooldusAeg { get; set; }
        public decimal SoetusMaksumus { get; set; }
        public decimal JaakMaksumus { get; set; }
        public bool Aktiivne { get; set; }
    }
}
