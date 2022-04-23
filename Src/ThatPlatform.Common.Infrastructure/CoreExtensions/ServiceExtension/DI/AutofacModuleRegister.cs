using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.Infrastructure.CommonAttributes;
using ThatPlatform.Infrastructure.ModuleManager;

namespace ThatPlatform.Infrastructure.ServiceExtension.DI
{
    /// <summary>
    /// AutofacModuleRegister
    /// </summary>
    public static class AutofacModuleRegister
    {
        public static void ModuleRegister(this ContainerBuilder builder)
        {
            var _modules = new ThatPlatformModulManager().LoadAllModules();

            foreach (Type type in _modules)
            {
                var depandAttribute = type.GetCustomAttribute<DependsOnAttribute>();
                if (depandAttribute?.DependedModuleTypes != null)
                {
                    foreach (var module in depandAttribute?.DependedModuleTypes)
                    {
                        //builder.RegisterGeneric(module).As(type).InstancePerDependency();

                        builder.RegisterType(module).As(type).InstancePerDependency();
                    }
                }
            }
        }
    }
}
