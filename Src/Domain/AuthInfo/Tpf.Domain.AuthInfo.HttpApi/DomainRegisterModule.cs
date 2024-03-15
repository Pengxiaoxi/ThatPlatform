using Autofac;
using Tpf.Autofac;
using Tpf.Domain.AuthInfo.Domain;
using Tpf.EntityFrameworkCore;

namespace Tpf.Domain.AuthInfo.HttpApi
{
    internal class DomainRegisterModule : AutofacRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 业务服务接口无需单独注册，已在 AutofacFactory 内批量注册
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();


            // 测试使用，待删除
            builder.RegisterType(typeof(AuthInfoDbContext)).InstancePerLifetimeScope();

            
        }
    }
}
