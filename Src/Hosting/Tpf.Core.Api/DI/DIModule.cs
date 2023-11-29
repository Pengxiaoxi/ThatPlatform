using Autofac;
using Tpf.Domain.Base.Application;
using Tpf.Domain.Base.Application.Contacts;
using Tpf.IOC;

namespace Tpf.Core.Api.DI
{
    /// <summary>
    /// 
    /// </summary>
    public class DIModule : DependencyInjectionModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 注册基础服务
            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerLifetimeScope();
            


            base.Load(builder);
        }
    }
}
