using Microsoft.AspNetCore.Mvc;

namespace demo02.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // 1. GET: api/products
        [HttpGet("api/products")]
        public IActionResult GetAllProducts()
        {
            return Ok(new List<string> { "Product1", "Product2" });
        }

        // 2. GET: api/products/{id}
        [HttpGet("api/products/{id}")]
        public IActionResult GetProductById(int id)
        {
            return Ok($"Product{id}");
        }

        // 3. POST: api/products
        [HttpPost("api/products")]
        public IActionResult CreateProduct([FromBody] string product)
        {
            // Simulate creation logic
            return CreatedAtAction(nameof(GetProductById), new { id = 1 }, product);
        }

        // 4. PUT: api/products/{id}
        [HttpPut("api/products/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] string product)
        {
            // Simulate update logic
            return NoContent();
        }

        // 5. DELETE: api/products/{id}
        [HttpDelete("api/products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            // Simulate delete logic
            return NoContent();
        }
    }
}
