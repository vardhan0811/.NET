using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare("orders", false, false, false);

var message = "Hello Order2";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish("", "orders", null, body);

Console.WriteLine("Sent: " + message);