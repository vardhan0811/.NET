using Microsoft.AspNetCore.Mvc;
using CatalogWebApi.Models;

namespace CatalogWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private static List<Product> products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 75000 },
        new Product { Id = 2, Name = "Mobile", Price = 25000 },
        new Product { Id = 3, Name = "Headphones", Price = 3000 }
    };

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = products.FirstOrDefault(x => x.Id == id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }
}