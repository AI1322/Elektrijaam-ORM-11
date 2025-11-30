using System.ComponentModel.DataAnnotations.Schema;

namespace Elektrijaam_ORM_11.Models
{
    public class Maksestaatus
    {
        public int Id { get; set; }
        public bool Makstud { get; set; }
        public DateTime MaksmiseTahtaeg { get; set; }
        public decimal MakstudSumma { get; set; }
        public DateTime? MaksmiseKuupaev { get; set; } = DateTime.Now;
    }
}
