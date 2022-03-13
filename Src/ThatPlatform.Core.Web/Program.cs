using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
            // 部署到Windows Service 或 Linux守护进程可启用此项【Nuget: Microsoft.Extensions.Hosting.WindowsServices】
            //.UseWindowsService() 
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseLog4Net();

    }
}
