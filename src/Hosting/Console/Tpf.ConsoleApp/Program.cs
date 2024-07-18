// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Tpf.Utils;

public class Program
{
    private static void Main(string[] args)
    {
        Consumer1();

        //Console.ReadLine();

        void Consumer1()
        {
            var factory = new ConnectionFactory
            {
                HostName = "42.192.5.10",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };
            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "SimpleQueue-durable",
                                 durable: true, // 是否持久化
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // 公平分发，一次不给工作进程超过一条消息（不在工作进程处理并确认前一条消息之前向其分发新消息）
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                //Console.WriteLine($" [x] Received {message}");
                ConsoleHelper.WriteColorLine($" [x] Received {message}", ConsoleColor.Green);

                // 手动发送消息确认信号
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: "SimpleQueue-durable",
                                 autoAck: false, // 是否开启自动消息确认
                                 consumer: consumer);

            Console.ReadKey();
            channel.Dispose();
            connection.Close();
        }
    }
}