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
        /// 1、Producer
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Producer([FromBody] string message)
        {
            if (_rabbitMqOptions is null || _rabbitMqOptions.Connections is null)
            {
                return false;
            }

            var factory = new ConnectionFactory 
            { 
                HostName = _rabbitMqOptions.Connections.Default.HostName, 
                Port = _rabbitMqOptions.Connections.Default.Port,
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "SimpleQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            //var body = Encoding.UTF8.GetBytes(message);

            //channel.BasicPublish(exchange: string.Empty,
            //                     routingKey: "SimpleQueue",
            //                     basicProperties: null,
            //                     body: body);

            //ConsoleHelper.WriteColorLine($" [x] Sent {message}", ConsoleColor.Yellow);


            // TEST
            for (int i = 0; i < 100; i++)
            {
                message = $"{i}";

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "SimpleQueue",
                                 basicProperties: null,
                                 body: body);

                ConsoleHelper.WriteColorLine($" [x] Sent {message}", ConsoleColor.Yellow);

                Thread.Sleep(1 * 1000);
            }

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 2、Consumer
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


    }
}
