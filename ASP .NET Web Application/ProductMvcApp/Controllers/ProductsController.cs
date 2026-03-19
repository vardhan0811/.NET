using Microsoft.AspNetCore.Mvc;
using ProductMvcApp.Models;
using System.Text.Json;

namespace ProductMvcApp.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _url = "https://dummyjson.com/products";

        public async Task<IActionResult> Index()
        {
            ProductResponse result = new ProductResponse();

            HttpResponseMessage response = await _client.GetAsync(_url);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<ProductResponse>(
                    data,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new ProductResponse();
            }

            return View(result.Products);
        }
    }
}