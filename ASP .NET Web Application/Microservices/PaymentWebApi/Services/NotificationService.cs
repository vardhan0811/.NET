using System.Net.Http.Json;

namespace PaymentWebApi.Services;

public class NotificationService
{
    private readonly HttpClient _httpClient;

    public NotificationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendNotification(string message)
    {
        var notification = new
        {
            Message = message,
            Type = "Email"
        };

        await _httpClient.PostAsJsonAsync(
            "https://localhost:7094/api/notification",
            notification);
    }
}