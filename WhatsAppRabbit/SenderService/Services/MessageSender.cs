using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using SenderService.Models;

namespace SenderService.Services
{

    public class MessageSender
    {
        public void Send(MessageDto message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "chat-queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish("", "chat-queue", null, body);

            Console.WriteLine($"Sent: {message.Text}");
        }
    }
}