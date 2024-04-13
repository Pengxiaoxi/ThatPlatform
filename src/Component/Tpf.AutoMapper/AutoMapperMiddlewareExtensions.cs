using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Tpf.AutoMapper
{
    public static class AutoMapperMiddlewareExtensions
    {
        /// <summary>
        /// AutoMapper Doc: https://docs.automapper.org/en/stable/Configuration.html
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperMiddleware(this IServiceCollection services)
        {
            var autoMapperProfileAssemblys = new List<Assembly>();

            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            if (assemblys.Any())
            {
                foreach (var assembly in assemblys)
                {
                    if (assembly.GetTypes().Any(x => x.IsSubclassOf(typeof(AutoMapperProfile))))
                    {
                        autoMapperProfileAssemblys.Add(assembly);
                    }
                }
            }

            if (autoMapperProfileAssemblys.Any())
            {
                //Assembly Scanning for auto configuration
                services.AddAutoMapper((config) =>
                {
                    config.AddMaps(autoMapperProfileAssemblys);
                });
            }
            
        }
    }
}
