using Elektrijaam_ORM_11.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Elektrijaam_ORM_11.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Seade> Seadmed { get; set; }
        public DbSet<Tarbija> Tarbijad { get; set; }
        public DbSet<Kontaktandmed> Kontaktandmed { get; set; }
        public DbSet<Asukoht> Asukohad { get; set; }
        public DbSet<Arve> Arved { get; set; }
        public DbSet<Maksestaatus> Maksestaatused { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
