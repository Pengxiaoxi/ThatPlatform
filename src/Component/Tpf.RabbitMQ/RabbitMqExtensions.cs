using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tpf.RabbitMQ
{
    public static class RabbitMqExtensions
    {
        /// <summary>
        /// AddRabbitMq
        /// </summary>
        /// <param name="builder"></param>
        public static void AddRabbitMq(this IHostApplicationBuilder builder)
        {
            // Configure Options (便于使用 IOptions<> 直接获取选项类)
            //builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(new RabbitMqOptions().SectionName));


            //var rabbitMqOptions = builder.Configuration.GetSection(new RabbitMqOptions().SectionName)?.Get<RabbitMqOptions>();


        }
    }
}
