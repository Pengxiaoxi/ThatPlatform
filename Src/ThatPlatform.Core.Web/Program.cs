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
            .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // ����ʹ��Autofac�滻IOC����
            // ����Windows Service �� Linux�ػ����̿����ô��Nuget: Microsoft.Extensions.Hosting.WindowsServices��
            //.UseWindowsService() 
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseLog4Net();

    }
}
