using Demo02.Models;

namespace Demo02.Repositories
{
    public interface IRepository
    {
        IEnumerable<Reservation> Reservations();
        Reservation Reservations(int id);
        Reservation AddReservation(Reservation reservation);
        Reservation UpdateReservation(Reservation reservation);
        void DeleteReservation(int id);
    }
}
