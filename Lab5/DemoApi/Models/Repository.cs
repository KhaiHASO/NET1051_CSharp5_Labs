using System.Collections.Generic;
using System.Linq;

namespace DemoApi.Models
{
    public class Repository : IRepository
    {
        private Dictionary<int, Reservation> items;

        public Repository()
        {
            items = new Dictionary<int, Reservation>();
            new List<Reservation> {
                new Reservation {Id=1, Name = "An", StartLocation = "Ha Noi", EndLocation="Hai Phong"},
                new Reservation {Id=2, Name = "Binh", StartLocation = "HCM", EndLocation="Vung Tau"},
                new Reservation {Id=3, Name = "Hoa", StartLocation = "Da Nang", EndLocation="Hue"}
            }.ForEach(r => Add(r));
        }

        public Reservation Add(Reservation reservation)
        {
            if (reservation.Id == 0)
            {
                int key = items.Count > 0 ? items.Keys.Max() + 1 : 1;
                reservation.Id = key;
            }
            items[reservation.Id] = reservation;
            return reservation;
        }

        public void Delete(int id)
        {
            items.Remove(id);
        }

        public IEnumerable<Reservation> GetAll() => items.Values;

        public Reservation GetById(int id) => items.ContainsKey(id) ? items[id] : null;

        public Reservation Update(Reservation reservation)
        {
             Add(reservation);
             return reservation;
        }
    }
}
