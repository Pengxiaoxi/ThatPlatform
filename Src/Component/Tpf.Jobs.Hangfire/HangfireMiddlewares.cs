using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;

namespace Tpf.Jobs.Hangfire
{
    /// <summary>
    /// Hangfire
    /// Docs: https://docs.hangfire.io/en/latest/getting-started/aspnet-core-applications.html
    /// </summary>
    public static class HangfireMiddlewares
    {
        /// <summary>
        /// AddTpfHangfire
        /// </summary>
        /// <param name="builder"></param>
        public static void AddTpfHangfire(this WebApplicationBuilder builder)
        {
            // Add Hangfire services.
            builder.Services.AddHangfire(configuration => configuration
                            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                            .UseSimpleAssemblyNameTypeSerializer()
                            .UseRecommendedSerializerSettings()
                            .UseMemoryStorage() // 测试使用 Hangfire.MemoryStorage，存储支持 SqlServer|Pgsql|Mysql|Redis|Mongo|MemoryCache

            // SqlServer Demo
            //.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"))
            );

            // Add the processing server as IHostedService
            builder.Services.AddHangfireServer();
        }

        public static void UseTpfHangfireMiddle(this WebApplication app)
        {
            app.UseHangfireDashboard();

            app.MapHangfireDashboard();

        }
    }
}
