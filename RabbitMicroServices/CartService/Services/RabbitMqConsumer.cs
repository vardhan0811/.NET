using CartService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CartService.Services
{
    public class RabbitMqConsumer
    {
        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("product-queue", false, false, false);

            var consumer = new EventingBasicConsumer(channel);

            var ap1 = new Validate();
            var ap2 = new AddToCart();
            var ap3 = new Calculate();
            var publisher = new RabbitMqPublisher();

            consumer.Received += (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                var product = JsonSerializer.Deserialize<ProductMessage>(json);

                // AP1
                ap1.Execute(product);

                // AP2
                var cart = ap2.Execute(product);

                // AP3
                cart = ap3.Execute(cart);

                // Send to Payment Service
                publisher.Publish(cart);
            };

            channel.BasicConsume("product-queue", true, consumer);
        }
    }
}
    