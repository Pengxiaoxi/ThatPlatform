using log4net;
using log4net.Config;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ThatPlatform.Core.Web
{
    public static class Log4NetExtensions
    {
        public static IHostBuilder UseLog4Net(this IHostBuilder hostBuilder)
        {
            var log4netRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(log4netRepository, new FileInfo($"{AppContext.BaseDirectory}/Log4Net/Log4Net.config"));

            return hostBuilder;
        }
    }

}
