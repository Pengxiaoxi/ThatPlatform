using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tpf.BlobStoring.Minio;
using Tpf.Common.ConfigOptions;

namespace Tpf.Middlewares.Options
{
    public static class OptionsExtensions
    {
        /// <summary>
        /// 配置选项类
        /// 1、为什么需要配置选项 ？ 在此处配置后可以直接使用 IOptionsSnapshot 注入配置的选项类
        /// 2、如果不配置选项该怎么去拿配置选项类 ？可通过 ConfigHelper.GetOptions
        /// </summary>
        /// <param name="services"></param>
        public static void AddTpfOptions(this IHostApplicationBuilder builder)
        {
            // You can user your setting key.
            //builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection(new MinioOptions().SectionName));

            //builder.Services.Configure<HttpApisOptions>(builder.Configuration.GetSection(HttpApisOptions.Name));


            #region TODO: 批量配置 Options
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


            //    builder.Services.Configure<>(builder.Configuration.GetSection(currentOptionsName));

            //    builder.Services.ConfigureOptions<>(builder.Configuration.GetSection(currentOptionsName));

            //} 
            #endregion

        }
    }
}
