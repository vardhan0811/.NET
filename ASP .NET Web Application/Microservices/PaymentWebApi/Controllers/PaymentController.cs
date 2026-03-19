using Microsoft.AspNetCore.Mvc;
using PaymentWebApi.Models;
using PaymentWebApi.Services;

namespace PaymentWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly CartService _cartService;
    private readonly NotificationService _notificationService;

    public PaymentController(
        CartService cartService,
        NotificationService notificationService)
    {
        _cartService = cartService;
        _notificationService = notificationService;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment()
    {
        var cart = await _cartService.GetCart();

        decimal total = 0;

        foreach (var item in cart)
        {
            total += item.Price * item.Quantity;
        }

        var payment = new Payment
        {
            OrderId = 101,
            Amount = total,
            Status = "Success"
        };

        await _notificationService.SendNotification(
            $"Payment successful. Amount: {total}");

        return Ok(payment);
    }
}