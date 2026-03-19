using Microsoft.AspNetCore.Mvc;
using CartWebApi.Models;
using CartWebApi.Services;

namespace CartWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly CatalogService _catalogService;

    public CartController(CatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    private static List<CartItem> cart = new()
    {
        new CartItem { ProductId = 1, Quantity = 2 },
        new CartItem { ProductId = 2, Quantity = 1 }
    };

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var result = new List<object>();

        foreach (var item in cart)
        {
            var product = await _catalogService.GetProduct(item.ProductId);

            result.Add(new
            {
                product?.Name,
                product?.Price,
                item.Quantity
            });
        }

        return Ok(result);
    }
}