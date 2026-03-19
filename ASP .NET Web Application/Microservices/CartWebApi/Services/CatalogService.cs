using System.Net.Http.Json;
using CartWebApi.Models;

namespace CartWebApi.Services;

public class CatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Product?> GetProduct(int id)
    {
        return await _httpClient.GetFromJsonAsync<Product>
        ($"https://localhost:7188/api/catalog/{id}");
    }
}

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
}