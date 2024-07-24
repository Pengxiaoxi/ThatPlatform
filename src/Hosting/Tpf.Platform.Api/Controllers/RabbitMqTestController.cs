using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Tpf.Domain.Base.HttpApi;
using Tpf.RabbitMQ;
using Tpf.Utils;

namespace Tpf.Platform.Api.Controllers
{
    /// <summary>
    /// RabbitMq Test
    /// </summary>
    [AllowAnonymous]
    public class RabbitMqTestController : BaseApiController
    {
        private readonly RabbitMqOptions _rabbitMqOptions;

        /// <summary>
        /// Ctor
        /// </summary>
        public RabbitMqTestController(IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _rabbitMqOptions = rabbitMqOptions.Value;
        }

        /// <summary>
        /// 1、工作队列 WorkQueueProducer
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkQueueProducer([FromBody] string message)
        {
            if (_rabbitMqOptions is null || _rabbitMqOptions.Connections is null)
            {
                return false;
            }

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.Connections.Default.HostName,
                Port = _rabbitMqOptions.Connections.Default.Port,
                // 无账户密码则默认使用 guest/guest 处理消息
                //UserName = "",
                //Password = "",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // 声明队列，durable: true 标记为持久性
            channel.QueueDeclare(queue: "SimpleQueue-durable",
                                 durable: true, // 是否持久化
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            // 将消息标记为持久性
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "SimpleQueue-durable",
                                 basicProperties: properties,
                                 body: body);

            ConsoleHelper.WriteColorLine($" [x] Sent {message}", ConsoleColor.Yellow);


            #region TEST
            // TEST
            //Task.Run(() =>
            //{
            //    var factory = new ConnectionFactory
            //    {
            //        HostName = _rabbitMqOptions.Connections.Default.HostName,
            //        Port = _rabbitMqOptions.Connections.Default.Port,
            //    };
            //    using var connection = factory.CreateConnection();
            //    using var channel = connection.CreateModel();

            //    channel.QueueDeclare(queue: "SimpleQueue",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    for (int i = 0; i < 100; i++)
            //    {
            //        message = $"{i}";

            //        var body = Encoding.UTF8.GetBytes(message);

            //        channel.BasicPublish(exchange: string.Empty,
            //                         routingKey: "SimpleQueue",
            //                         basicProperties: null,
            //                         body: body);

            //        ConsoleHelper.WriteColorLine($" [x] Sent {message}", ConsoleColor.Yellow);

            //        Thread.Sleep(1 * 1000);
            //    }
            //}); 
            #endregion

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 2、Fanout 广播交换机
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> FanoutProducer([FromBody] string message)
        {
            if (_rabbitMqOptions is null || _rabbitMqOptions.Connections is null)
            {
                return false;
            }

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.Connections.Default.HostName,
                Port = _rabbitMqOptions.Connections.Default.Port,
                // 无账户密码则默认使用 guest/guest 处理消息
                //UserName = "",
                //Password = "",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // 指定交换机名称及其类型
            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "logs",
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: body);

            ConsoleHelper.WriteColorLine($" [x] Sent {message}", ConsoleColor.Yellow);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 3、Direct 直接交换机
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DirectProducer([FromBody] string[] args)
        {
            if (_rabbitMqOptions is null || _rabbitMqOptions.Connections is null)
            {
                return false;
            }

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.Connections.Default.HostName,
                Port = _rabbitMqOptions.Connections.Default.Port,
                // 无账户密码则默认使用 guest/guest 处理消息
                //UserName = "",
                //Password = "",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // 指定交换机名称及其类型
            channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

            var severity = (args.Length > 0) ? args[0] : "info";
            var message = (args.Length > 1)
              ? string.Join(" ", args.Skip(1).ToArray())
              : "Hello World!";

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "direct_logs",
                                 routingKey: severity,
                                 basicProperties: null,
                                 body: body);

            ConsoleHelper.WriteColorLine($" [x] Sent '{severity}':'{message}'", ConsoleColor.Yellow);

            return await Task.FromResult(true);
        }


        /// <summary>
        /// 4、Topic 主题交换机
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> TopicProducer([FromBody] string[] args)
        {
            if (_rabbitMqOptions is null || _rabbitMqOptions.Connections is null)
            {
                return false;
            }

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.Connections.Default.HostName,
                Port = _rabbitMqOptions.Connections.Default.Port,
                // 无账户密码则默认使用 guest/guest 处理消息
                //UserName = "",
                //Password = "",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // 指定交换机名称及其类型
            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

            var routingKey = (args.Length > 0) ? args[0] : "anonymous.info";
            var message = (args.Length > 1)
              ? string.Join(" ", args.Skip(1).ToArray())
              : "Hello World!";

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "topic_logs",
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);

            ConsoleHelper.WriteColorLine($" [x] Sent '{routingKey}':'{message}'", ConsoleColor.Yellow);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 5、RPC 远程调用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> RpcServer()
        {
            var result = await new RpcClient(_rabbitMqOptions).CallAsync("10");

            Console.WriteLine(result);

            return true;
        }


        /// <summary>
        /// 20、Consumer
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Consumer()
        {
            //var factory = new ConnectionFactory { HostName = _rabbitMqOptions.Connections.Default.HostName };
            //using var connection = factory.CreateConnection();

            //using var channel = connection.CreateModel();

            //channel.QueueDeclare(queue: "SimpleQueue",
            //                     durable: false,
            //                     exclusive: false,
            //                     autoDelete: false,
            //                     arguments: null);

            //Console.WriteLine(" [*] Waiting for messages.");

            //var consumer = new EventingBasicConsumer(channel);
            //consumer.Received += (model, ea) =>
            //{
            //    var body = ea.Body.ToArray();
            //    var message = Encoding.UTF8.GetString(body);
            //    //Console.WriteLine($" [x] Received {message}");
            //    ConsoleHelper.WriteSuccessLine($" [x] Received {message}");
            //};
            //channel.BasicConsume(queue: "SimpleQueue",
            //                     autoAck: true,
            //                     consumer: consumer);

            return await Task.FromResult(true);
        }


        #region Private Method
        
        #endregion
    }
}
