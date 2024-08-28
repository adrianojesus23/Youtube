using BaseDadosConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseDadosConnections.Resources
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Vendor> Vendors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Invoice>()
            //    .HasOne(i => i.Vendor)
            //    .WithMany(v => v.Invoices)
            //    .HasForeignKey(i => i.VendorID);

            base.OnModelCreating(modelBuilder);
        }
    }
}