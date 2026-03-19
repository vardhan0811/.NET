using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using CartService.Models;

namespace CartService.Services
{
    public class CartSender
    {
        public void Send(CartDto cart)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("payment-queue", false, false, false);

            var json = JsonSerializer.Serialize(cart);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish("", "payment-queue", null, body);

            Console.WriteLine("Cart sent to PaymentService");
        }
    }
}