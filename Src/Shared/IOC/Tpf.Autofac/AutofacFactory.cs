using Autofac;
using Tpf.Utils.AssemblyHelpers;

namespace Tpf.Autofac
{
    public static class AutofacFactory
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

        public static void RegisterConfigure(this ContainerBuilder containerBuilder)
        {
            // Load并获取当前运行路径下 tpf 相关程序集
            var assemblies = AssemblyHelper.GetSolutionAssemblies()
                .Where(x => x.FullName != null && x.FullName.StartsWith("tpf", StringComparison.CurrentCultureIgnoreCase))
                .ToArray();

            // 按程序集批量注册模块（按Key|Name注册、泛型接口、或其他生命周期的服务等）
            containerBuilder.RegisterAssemblyModules<AutofacModule>(assemblies);

            // 批量注册非DI模块内的服务（服务生命周期默认瞬时）
            containerBuilder.RegisterAssemblyTypes(assemblies)
                          .AsImplementedInterfaces()
                          .InstancePerDependency()
                          .PropertiesAutowired();
            //.EnableInterfaceInterceptors(); // 如需使用 Interceptor 引用Autofac.Extras.DynamicProxy;

            #region Tips: 按程序集批量注册模块 等同于如下（获取程序集内所有模块然后逐个注册）
            //foreach (Type type in modules)
            //{
            //    var module = Activator.CreateInstance(type) as AutofacModule;
            //    if (module != null)
            //    {
            //        containerBuilder.RegisterModule(module);
            //    }
            //}
            #endregion

            // 设置 container（便于后续手动注入使用）
            containerBuilder.RegisterBuildCallback(container =>
            {
                SetContainer((IContainer)container);
            });
        }
    }
}
