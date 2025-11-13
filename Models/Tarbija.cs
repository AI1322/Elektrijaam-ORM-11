using System.ComponentModel.DataAnnotations.Schema;

namespace Elektrijaam_ORM_11.Models
{
    public class Tarbija
    {
        public int Id { get; set; }
        public string Nimi { get; set; } = string.Empty;
        public Kontaktandmed Kontaktandmed { get; set; }
        public Asukoht Asukoht { get; set; }
        public ICollection<Arve> Arved { get; set; } = new List<Arve>();
    }
}
