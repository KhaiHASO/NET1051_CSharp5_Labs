using System.Collections.Generic;

namespace DemoApi.Models
{
    public interface IRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        Reservation Add(Reservation reservation);
        Reservation Update(Reservation reservation);
        void Delete(int id);
    }
}
