using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.DTOs;
using System.Text.Json;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    private static List<Order> orders = new();

    public OrdersController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        var client = _httpClientFactory.CreateClient();

        // Call ProductService for specific product
        var response = await client.GetAsync($"https://localhost:7001/api/products/{dto.ProductId}");

        if (!response.IsSuccessStatusCode)
            return BadRequest("Product not found");

        var json = await response.Content.ReadAsStringAsync();

        var product = JsonSerializer.Deserialize<ProductDto>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        var order = new Order
        {
            Id = orders.Count + 1,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };

        orders.Add(order);

        return Ok(order);
    }
}