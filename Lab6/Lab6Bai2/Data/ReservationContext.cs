using Lab6Bai2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6Bai2.Data
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options) { }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
