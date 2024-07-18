using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Tpf.Common.Options;

namespace Tpf.Middlewares.Options
{
    public static class OptionsExtensions
    {
        /// <summary>
        /// 选项模式-配置选项类
        /// 1、为什么需要配置选项 ？ 在此处配置后可以直接使用 IOptionsSnapshot 注入配置的选项类
        /// 2、如果不配置选项该怎么去拿配置选项类 ？可通过 ConfigHelper.GetOptions
        /// </summary>
        public static void AddOptions(this IHostApplicationBuilder builder)
        {
            // You can user your setting key.
            //builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection(new MinioOptions().SectionName));

            //builder.Services.Configure<HttpApisOptions>(builder.Configuration.GetSection(HttpApisOptions.Name));


            #region TODO: 批量配置 Options
            var optionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsAssignableTo(typeof(BaseOptions))
                            && x.IsClass
                            && !x.IsAbstract)
                .ToList();

            foreach (var type in optionTypes)
            {
                var instance = Activator.CreateInstance(type);
                var currentOptionsName = (instance as BaseOptions)?.SectionName;

                var option = builder.Configuration.GetSection(currentOptionsName).Get(type);
                if (option is not null)
                {
                    //builder.Services.ConfigureOptions(option);

                    //var TType = type.BaseType;
                    //builder.Services.Configure<dynamic>(builder.Configuration.GetSection(currentOptionsName));

                    //builder.Services.AddOptions<BaseOptions>(currentOptionsName);
                }
                
                //builder.Services.Configure<BaseOptions>(builder.Configuration.GetSection(currentOptionsName));
            }
            #endregion

        }
    }
}
