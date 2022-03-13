using log4net;
using log4net.Config;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace ThatPlatform.Common.Infrastructure.HostBuilderExtension.Log4Net
{
    public static class Log4NetExtensions
    {
        /// <summary>
        /// UseLog4Net
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder UseLog4Net(this IHostBuilder hostBuilder)
        {
            var log4netRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(log4netRepository, new FileInfo($"{AppContext.BaseDirectory}/Log4Net/Log4Net.config"));

            return hostBuilder;
        }
    }
}
