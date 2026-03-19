using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Services;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductSender _sender = new();

        [HttpPost]
        public IActionResult Send(ProductDto product)
        {
            _sender.Send(product);
            return Ok("Product sent to CartService");
        }
    }
}