using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductSender
    {
        public void Send(ProductDto product)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("product-queue", false, false, false);

            var json = JsonSerializer.Serialize(product);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish("", "product-queue", null, body);

            Console.WriteLine($"Sent Product: {product.Name}");
        }
    }
}