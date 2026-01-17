using DemoApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository repository;

        public ReservationController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (id == 0) return BadRequest("Value must be passed in the request body.");
            return Ok(repository.GetById(id));
        }

        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
            repository.Add(res);

        [HttpPut]
        public Reservation Put([FromBody] Reservation res) =>
            repository.Update(res);

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.Delete(id);

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Reservation> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var res = repository.GetById(id);
            if (res == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(res, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(res);
        }
    }
}
