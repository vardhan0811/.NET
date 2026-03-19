using Microsoft.AspNetCore.Mvc;
using ProductService.Services;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _service = new();
        private readonly RabbitMqPublisher _publisher = new();

        [HttpPost("{id}")]
        public IActionResult Send(int id)
        {
            var product = _service.GetProduct(id);

            _publisher.Publish(product);

            return Ok("Product sent to Cart Service");
        }
    }
}
