using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tpf.Common.ConfigOptions;
using Tpf.Utils;

namespace Tpf.Middlewares.Options
{
    public static class OptionsExtensions
    {
        /// <summary>
        /// 配置选项类
        /// </summary>
        /// <param name="services"></param>
        public static void AddTpfOptions(this IServiceCollection services)
        {
            // 配置当前名称的选项类
            services.Configure<BlobStoringOptions>(ConfigHelper.GetSection(BlobStoringOptions.Name));

            services.Configure<MinioOptions>(ConfigHelper.GetSection(MinioOptions.Name));

            // TODO: 批量配置 Options
            //var optionTypes = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(x => x.GetTypes())
            //    .Where(x => x.IsAssignableFrom(typeof(IBaseOptions))
            //                && x.IsClass
            //                && !x.IsAbstract)
            //    .ToList();
            //foreach (var type in optionTypes)
            //{
            //    var instance = Activator.CreateInstance(type);
            //    var currentOptionsName = (instance as IBaseOptions)?.CurrentOptionsName;

            //    services.Configure<type>(ConfigHelper.GetSection(currentOptionsName));

            //}

        }
    }
}
