using Autofac;
using Microsoft.Extensions.Hosting;

namespace Tpf.Autofac
{
    public class AutofacFactory
    {
        private static IContainer? _container;

        public static IContainer GetContainer()
        {
            return _container;
        }

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static void RegisterConfigureAction(HostBuilderContext hostBuilder, ContainerBuilder containerBuilder)
        {
            var autofacModuleType = typeof(AutofacModule);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var modules = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => x.IsAssignableTo(autofacModuleType) && x != autofacModuleType && x.IsClass && !x.IsAbstract)
                .ToList();

            //foreach (Type type in modules)
            //{
            //    var module = Activator.CreateInstance(type) as AutofacModule;
            //    if (module != null)
            //    {
            //        containerBuilder.RegisterModule(module);
            //    }
            //}
            containerBuilder.RegisterAssemblyModules<AutofacModule>(assemblies);


            //var assemblyCollection = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName != null && x.FullName.ToLower().StartsWith("tpf."));
            //foreach (var assembly in assemblyCollection)
            //{
            //    //var startModule = assembly.GetExportedTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IModuleByAutoFac)));
            //    //if (startModule == null) continue;

            //    //var module = Activator.CreateInstance(startModule, null) as IModuleByAutoFac;

            //    //module?.ResolveType(container);
            //    //module?.RegisterType(container);

            //    containerBuilder.RegisterAssemblyModules(assembly);
            //}

            containerBuilder.RegisterBuildCallback(container =>
            {
                SetContainer((IContainer)container);
            });
        }

        #region Other way
        //public static void RegisterConfigureAction(ContainerBuilder container)
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
        #endregion

    }
}
