using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tpf.Core.CommonAttributes;
using Tpf.Core.ModuleManager;

namespace Tpf.Core.ServiceExtension.DI
{
    public static class ServicesCollectionExtension
    {
        /// <summary>
        /// 依赖注入（反射实现）
        /// </summary>
        /// <param name="services"></param>
        public static void AddModules(this IServiceCollection services)
        {
            var _modules = new ThatPlatformModulManager().LoadAllModules();
            foreach (Type type in _modules)
            {
                var depandAttribute = type.GetCustomAttribute<DependsOnAttribute>();
                if (depandAttribute?.DependedModuleTypes != null)
                {
                    foreach (var module in depandAttribute?.DependedModuleTypes)
                    {
                        services.AddTransient(module, type);
                    }
                }
            }
        }
    }
}
