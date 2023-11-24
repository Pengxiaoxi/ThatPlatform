using Autofac;
using System.Reflection;
using Tpf.Autofac;

namespace Tpf.Domain.AuthInfo.HttpApi
{
    internal class DependencyModule : AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.StartsWith("Tpf"))
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
