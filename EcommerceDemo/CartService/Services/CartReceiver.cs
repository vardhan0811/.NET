using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CartService.Models;

namespace CartService.Services
{
    public class CartReceiver
    {
        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("product-queue", false, false, false);

            var consumer = new EventingBasicConsumer(channel);

            var ap1 = new CartAp1();
            var ap2 = new CartAp2();
            var ap3 = new CartAp3();
            var sender = new CartSender();

            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("\n===== NEW MESSAGE =====");

                // 🔵 UNACK START
                Console.WriteLine($"[UNACK START] DeliveryTag: {ea.DeliveryTag}");

                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Raw Message: {json}");

                var product = JsonSerializer.Deserialize<ProductDto>(json)!;

                Console.WriteLine("[PROCESSING START]");

                // AP1
                ap1.Execute(product);

                // AP2
                var cart = ap2.Execute(product);

                // AP3
                cart = ap3.Execute(cart);

                Console.WriteLine("[PROCESSING END]");

                // ⏳ Delay to observe UNACK
                Thread.Sleep(5000);

                // 🟢 ACK
                channel.BasicAck(ea.DeliveryTag, false);

                Console.WriteLine($"[ACK SENT] DeliveryTag: {ea.DeliveryTag}");
                Console.WriteLine("===== MESSAGE COMPLETED =====\n");

                sender.Send(cart);
            };

            channel.BasicConsume("product-queue", true, consumer);
        }
    }
}