using Autofac;
using Microsoft.Extensions.Hosting;

namespace Tpf.Autofac
{
    public class AutofacFactory
    {
        private static IContainer? _container;

        public static IContainer GetFCContainer()
        {
            return _container;
        }

        public static void SetFCContainer(IContainer container)
        {
            _container = container;
        }

        //public static void Register(ContainerBuilder container)
        //{
        //    var assemblyCollection = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName != null && x.FullName.ToLower().StartsWith("tpf."));
        //    foreach (var assembly in assemblyCollection)
        //    {
        //        var startModule = assembly.GetExportedTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IModuleByAutoFac)));
        //        if (startModule == null) continue;

        //        var module = Activator.CreateInstance(startModule, null) as IModuleByAutoFac;

        //        module?.ResolveType(container);
        //        module?.RegisterType(container);
        //    }
        //}


        public static void Register(HostBuilderContext hostBuilder, ContainerBuilder containBuilder)
        {
            var assemblyCollection = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName != null && x.FullName.ToLower().StartsWith("tpf."));
            foreach (var assembly in assemblyCollection)
            {
                //var startModule = assembly.GetExportedTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IModuleByAutoFac)));
                //if (startModule == null) continue;

                //var module = Activator.CreateInstance(startModule, null) as IModuleByAutoFac;

                //module?.ResolveType(container);
                //module?.RegisterType(container);

                containBuilder.RegisterAssemblyModules(assembly);
            }
        }

    }
}
