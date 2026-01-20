using Demo02.Data;
using Demo02.Models;

namespace Demo02.Repositories
{
    public class Repository : IRepository
    {
        private readonly ConsumeClientContext _context;

        public Repository(ConsumeClientContext context)
        {
            _context = context;
        }

        public Reservation AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }

        public void DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if(reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Reservation> Reservations()
        {
            return _context.Reservations.ToList();
        }

        public Reservation Reservations(int id)
        {
            return _context.Reservations.Find(id);
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
            return reservation;
        }
    }
}
