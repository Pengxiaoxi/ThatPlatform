using Autofac;
using Tpf.Autofac;
using Tpf.Domain.AuthInfo.Domain;

namespace Tpf.Domain.AuthInfo.HttpApi
{
    internal class DIModule : AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(BaseInfoDbContext)).InstancePerLifetimeScope();

            // 业务服务接口无需单独注册，已在 AutofacFactory 内批量注册
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();

            //builder.RegisterType(typeof(TpfDbContextBase)).InstancePerLifetimeScope();
        }
    }
}
