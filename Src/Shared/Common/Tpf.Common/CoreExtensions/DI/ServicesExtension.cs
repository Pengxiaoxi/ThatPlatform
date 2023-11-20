using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Tpf.Common.CommonAttributes;
using Tpf.Common.CoreExtensions.ModuleManager;

namespace Tpf.Common.CoreExtensions.DI
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
