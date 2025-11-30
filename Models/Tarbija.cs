using System.ComponentModel.DataAnnotations.Schema;

namespace Elektrijaam_ORM_11.Models
{
    public class Tarbija
    {
        public int Id { get; set; }
        public string Nimi { get; set; } = string.Empty;
        public int KontaktandmedId { get; set; }
        public Kontaktandmed? Kontaktandmed { get; set; }

        public int AsukohtId { get; set; }
        public Asukoht? Asukoht { get; set; }
    }
}
