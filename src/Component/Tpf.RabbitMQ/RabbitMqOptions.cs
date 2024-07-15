using Tpf.Common.ConfigOptions;

namespace Tpf.RabbitMQ
{
    public class RabbitMqOptions : BaseOptions
    {
        public override string SectionName => "RabbitMQ";

        public RabbitMqConnections Connections { get; }

        

        public RabbitMqOptions() 
        {
            Connections = new RabbitMqConnections();
        }

    }
}
