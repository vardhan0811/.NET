using Microsoft.AspNetCore.Mvc;
using SenderService.Models;
using SenderService.Services;

namespace SenderService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly MessageSender _sender = new();

        [HttpPost]
        public IActionResult Send(MessageDto message)
        {
            _sender.Send(message);
            return Ok("Message sent");
        }
    }
}