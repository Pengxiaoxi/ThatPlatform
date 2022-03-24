using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ThatPlatform.Infrastructure.HostBuilderExtension.Log4Net;

namespace ThatPlatform.Core.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // 设置使用Autofac替换IOC容器
            // 部署到Windows Service 或 Linux守护进程可启用此项【Nuget: Microsoft.Extensions.Hosting.WindowsServices】
            //.UseWindowsService() 
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseLog4Net();

    }
}
