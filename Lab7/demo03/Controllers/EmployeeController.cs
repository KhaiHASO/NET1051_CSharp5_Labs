using Microsoft.AspNetCore.Mvc;

namespace demo03.Controllers
{
    // NO [ApiController] attribute to allow pure conventional routing flexibility
    public class EmployeeController : ControllerBase
    {
        // Action 1: Attribute Routing
        // URL: /Attribute/Path
        [Route("Attribute/Path")]
        [HttpGet]
        public IActionResult GetByAttribute()
        {
            return Ok("Response from GetByAttribute Method (Attribute Routing)");
        }

        // Action 2: Conventional Routing
        // URL: /api/Employee/GetByConvention
        [HttpGet] // Optional: limits to GET, but route is determined by Program.cs pattern
        public IActionResult GetByConvention()
        {
            return Ok("Response from GetByConvention Method (Conventional Routing)");
        }
    }
}
