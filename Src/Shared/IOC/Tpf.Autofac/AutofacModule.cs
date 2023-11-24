using Autofac;
using Tpf.BaseRepository;
using Tpf.Domain.Base.Application;

namespace Tpf.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerDependency();//注册基础服务

            base.Load(builder);
        }
    }
}
