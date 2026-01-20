using Demo02.Models;
using Demo02.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository _repository;

        public ReservationController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Reservation> Get() => _repository.Reservations();

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            var reservation = _repository.Reservations(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult<Reservation> Post([FromBody] Reservation reservation)
        {
           return _repository.AddReservation(reservation);
        }

        [HttpPut]
        public ActionResult<Reservation> Put([FromBody] Reservation reservation) => _repository.UpdateReservation(reservation);

        [HttpDelete("{id}")]
        public void Delete(int id) => _repository.DeleteReservation(id);
    }
}
