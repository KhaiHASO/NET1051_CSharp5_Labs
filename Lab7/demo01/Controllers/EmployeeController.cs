using Microsoft.AspNetCore.Mvc;

namespace demo01.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Action 1: GetAllEmployees
        [Route("Emp/All")]
        [HttpGet]
        public string GetAllEmployees()
        {
            return "Response from GetAllEmployees Method";
        }

        // Action 2: GetEmployeeById
        [Route("Emp/ById/{Id}")]
        [HttpGet]
        public string GetEmployeeById(int Id)
        {
            return $"Response from GetEmployeeById Method Id: {Id}";
        }
    }
}
