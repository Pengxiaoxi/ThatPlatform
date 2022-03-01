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
            // ����Windows Service �� Linux�ػ����̿����ô��Nuget: Microsoft.Extensions.Hosting.WindowsServices��
            //.UseWindowsService() 
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseLog4Net();

    }
}
