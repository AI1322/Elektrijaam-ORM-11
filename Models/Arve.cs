using System.ComponentModel.DataAnnotations.Schema;

namespace Elektrijaam_ORM_11.Models
{
    public class Arve
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public double TarbimiseKogus { get; set; }
        public decimal Summa { get; set; }
        public Maksestaatus Maksestaatus { get; set; }
        public Tarbija Tarbija { get; set; }
    }
}
