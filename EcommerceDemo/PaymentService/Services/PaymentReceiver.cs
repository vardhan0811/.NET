using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using PaymentService.Models;

namespace PaymentService.Services
{
    public class PaymentReceiver
    {
        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("payment-queue", false, false, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var cart = JsonSerializer.Deserialize<CartDto>(json)!;

                Console.WriteLine($"Payment Done: {cart.TotalAmount}");
            };

            channel.BasicConsume("payment-queue", true, consumer);
        }
    }
}