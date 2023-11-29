using Autofac;
using Tpf.IOC;

namespace Tpf.Grpc.Client
{
    public class DIModule : DependencyInjectionModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GrpcService>().As<IGrpcService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
