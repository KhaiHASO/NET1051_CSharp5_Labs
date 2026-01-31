using Microsoft.EntityFrameworkCore;

namespace Demo02.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed Data mẫu để test
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, Name = "Nguyen Van A", StartLocation = "Ha Noi", EndLocation = "Ho Chi Minh" },
                new Reservation { Id = 2, Name = "Tran Thi B", StartLocation = "Da Nang", EndLocation = "Hai Phong" },
                new Reservation { Id = 3, Name = "Le Van C", StartLocation = "Can Tho", EndLocation = "Vung Tau" }
            );
        }
    }
}
