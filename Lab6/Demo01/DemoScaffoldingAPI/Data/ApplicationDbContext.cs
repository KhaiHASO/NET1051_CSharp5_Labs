using DemoScaffoldingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoScaffoldingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API to ensure IsUnicode(false) corresponds to varchar in SQL Server
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.StartLocation)
                .IsUnicode(false);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.EndLocation)
                .IsUnicode(false);
        }
    }
}
