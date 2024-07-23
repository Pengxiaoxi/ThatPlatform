// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Tpf.Utils;

public class Program
{
    public static void Main(string[] args)
    {
        //WorkQueueConsumer1();


        //FanoutConsumer1();


        //DirectConsumer1();


        TopicConsumer1();


    }

    /// <summary>
    /// 工作队列 消费者1
    /// https://rabbitmq.org.cn/tutorials/tutorial-two-dotnet
    /// </summary>
    public static void WorkQueueConsumer1()
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


    /// <summary>
    /// 发布/订阅 Fanout 广播 消费者1
    /// https://rabbitmq.org.cn/tutorials/tutorial-three-dotnet#exchanges
    /// </summary>
    public static void FanoutConsumer1()
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

        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(
            queue: queueName, 
            exchange: "logs",
            routingKey: string.Empty
            );

        //// 公平分发，一次不给工作进程超过一条消息（不在工作进程处理并确认前一条消息之前向其分发新消息）
        //channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            //Console.WriteLine($" [x] Received {message}");
            ConsoleHelper.WriteColorLine($" [x] Received {message}", ConsoleColor.Green);
        };
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

        Console.ReadKey();
        channel.Dispose();
        connection.Close();
    }


    /// <summary>
    /// 发布/订阅 直接交换 消费者1
    /// https://rabbitmq.org.cn/tutorials/tutorial-four-dotnet
    /// </summary>
    public static void DirectConsumer1()
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

        channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

        var queueName = channel.QueueDeclare().QueueName;

        var args = new[] { "info", "error" };
        foreach (var severity in args)
        {
            channel.QueueBind(
                queue: queueName,
                exchange: "direct_logs",
                routingKey: severity
            );
        }
        
        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            ConsoleHelper.WriteColorLine($" [x] Received {ea.RoutingKey}: {message}", ConsoleColor.Green);
        };
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

        Console.ReadKey();
        channel.Dispose();
        connection.Close();
    }


    /// <summary>
    /// 发布/订阅 主题交换机 消费者1
    /// https://rabbitmq.org.cn/tutorials/tutorial-five-dotnet
    /// </summary>
    public static void TopicConsumer1()
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

        channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

        var queueName = channel.QueueDeclare().QueueName;

        //var args = new[] { "#" }; // 全部 等同于 Fanout
        //var args = new[] { "anonymous" }; // 等同于 Direct
        //var args = new[] { "anonymous.*" }; // 以"anonymous"开头 * 一个单词
        var args = new[] { "#.anonymous" }; // 以"anonymous"结尾 # 一个或多个单词
        foreach (var routingKey in args)
        {
            channel.QueueBind(
                queue: queueName,
                exchange: "topic_logs",
                routingKey: routingKey
            );
        }

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            ConsoleHelper.WriteColorLine($" [x] Received {ea.RoutingKey}: {message}", ConsoleColor.Green);
        };
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

        Console.ReadKey();
        channel.Dispose();
        connection.Close();
    }



}