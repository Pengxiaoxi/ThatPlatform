using Autofac;
using System;
using System.Reflection;
using Tpf.Common.CommonAttributes;
using Tpf.Common.CoreExtensions.ModuleManager;

namespace Tpf.Common.CoreExtensions.DI
{
    /// <summary>
    /// AutofacModuleRegister
    /// </summary>
    public static class AutofacModuleRegister
    {
        public static void ModuleRegister(this ContainerBuilder builder)
        {
            //var _modules = new ThatPlatformModulManager().LoadAllModules();

            //foreach (Type type in _modules)
            //{
            //    var depandAttribute = type.GetCustomAttribute<DependsOnAttribute>();
            //    if (depandAttribute?.DependedModuleTypes != null)
            //    {
            //        foreach (var module in depandAttribute?.DependedModuleTypes)
            //        {
            //            //builder.RegisterGeneric(module).As(type).InstancePerDependency();

            //            builder.RegisterType(module).As(type).InstancePerDependency();
            //        }
            //    }
            //}
        }
    }
}
