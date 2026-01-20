using Demo02.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Data
{
    public class ConsumeClientContext : DbContext
    {
        public ConsumeClientContext(DbContextOptions<ConsumeClientContext> options) : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Manual Fluent API Configuration
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Name)
                .HasMaxLength(250)
                .IsUnicode(false);

            modelBuilder.Entity<Reservation>()
                 .Property(r => r.StartLocation)
                 .HasMaxLength(250)
                 .IsUnicode(false);

            modelBuilder.Entity<Reservation>()
                 .Property(r => r.EndLocation)
                 .HasMaxLength(250)
                 .IsUnicode(false);
        }
    }
}
