using System.Net.Http.Json;

namespace PaymentWebApi.Services;

public class CartService
{
    private readonly HttpClient _httpClient;

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CartItem>?> GetCart()
    {
        return await _httpClient.GetFromJsonAsync<List<CartItem>>(
            "https://localhost:7146/api/cart");
    }
}

public class CartItem
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}