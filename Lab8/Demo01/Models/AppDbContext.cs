using Microsoft.EntityFrameworkCore;

namespace Demo01.Models
{
    // Mô phỏng Slide 20 (Cấu hình DbContext và Seed Data)
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data tự động nếu bảng rỗng
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, Name = "An Nguyen", StartLocation = "Ha Noi", EndLocation = "Ho Chi Minh" },
                new Reservation { Id = 2, Name = "Binh Tran", StartLocation = "Da Nang", EndLocation = "Nha Trang" },
                new Reservation { Id = 3, Name = "Chi Le", StartLocation = "Hai Phong", EndLocation = "Quang Ninh" }
            );
        }
    }
}
