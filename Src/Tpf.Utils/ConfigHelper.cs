using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Utils
{
    public static class ConfigHelper
    {
        #region Field
        private static IConfiguration _configuration;
        #endregion

        static ConfigHelper()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true }) //可以直接读目录里的json文件，修改后自动生效
                .Build();
        }

        /// <summary>
        /// GetConfig
        /// </summary>
        /// <param name="configSectionName"></param>
        /// <returns></returns>
        public static string GetConfig(string configSectionName)
        {
            if(string.IsNullOrEmpty(configSectionName))
            {
                return string.Empty;
            }

            var section = _configuration.GetSection(configSectionName);
            if (section != null)
            {
                return section.Value;
            }
            return string.Empty;
        }
    }
}
