using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReceiverService.Models;

namespace ReceiverService.Services
{
    public class MessageReceiver
    {
        public void Start()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "chat-queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("---- MESSAGE RECEIVED ----");

                // Step 1: Message arrived (UNACK state starts here)
                Console.WriteLine($"[UNACK START] DeliveryTag: {ea.DeliveryTag}");

                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message Body: {json}");

                // Simulate processing
                Console.WriteLine("[PROCESSING...]");
                Thread.Sleep(10000);

                // Step 2: ACK
                channel.BasicAck(ea.DeliveryTag, false);

                Console.WriteLine($"[ACK SENT] DeliveryTag: {ea.DeliveryTag}");

                Console.WriteLine("---- MESSAGE COMPLETED ----\n");
            };

            channel.BasicConsume(
                queue: "chat-queue",
                autoAck: false,
                consumer: consumer);
        }
    }
}