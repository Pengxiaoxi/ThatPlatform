using Autofac;
using Tpf.BaseRepository;
using Tpf.Domain.Base.Application;

namespace Tpf.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
