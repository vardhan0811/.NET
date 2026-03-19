using RabbitMQ.Client;
using System.Text;

namespace RabbitDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync("testqueue", false, false, false);

            var message = "Hello from C#";
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync("", "testqueue", false, body);

            Console.WriteLine("Message Sent!");
        }
    }
}