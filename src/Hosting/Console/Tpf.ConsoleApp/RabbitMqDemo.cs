using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Tpf.Utils;

namespace Tpf.ConsoleApp
{
    public class RabbitMqDemo
    {
        public async Task<bool> Consumer()
        {
            var factory = new ConnectionFactory 
            { 
                HostName = "42.192.5.10", 
                Port = 5672 
            };
            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "SimpleQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //Console.WriteLine($" [x] Received {message}");
                ConsoleHelper.WriteSuccessLine($" [x] Received {message}");
            };
            channel.BasicConsume(queue: "SimpleQueue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadKey();
            channel.Dispose();
            connection.Close();

            return await Task.FromResult(true);
        }
    }
}
