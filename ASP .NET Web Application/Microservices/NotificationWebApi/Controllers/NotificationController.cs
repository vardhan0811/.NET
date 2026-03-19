using Microsoft.AspNetCore.Mvc;
using NotificationWebApi.Models;

namespace NotificationWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    [HttpPost]
    public IActionResult SendNotification(Notification notification)
    {
        return Ok(new
        {
            Status = "Notification Sent",
            notification.Message,
            notification.Type
        });
    }
}