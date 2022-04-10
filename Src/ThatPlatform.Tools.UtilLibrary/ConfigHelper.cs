using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Tools.UtilLibrary
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
        /// GetConfig TODO: 待测试
        /// </summary>
        /// <param name="masterName"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static string GetConfig(string masterName, string sectionName = null)
        {
            if(string.IsNullOrEmpty(masterName))
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(masterName) && string.IsNullOrEmpty(sectionName))
            {
                return _configuration[masterName];
            }

            if (!string.IsNullOrEmpty(masterName) && !string.IsNullOrEmpty(sectionName))
            {
                var allChildrens = _configuration.GetSection(masterName).GetChildren();
                if (allChildrens.Any(x => x.Key == sectionName))
                {
                    return allChildrens.FirstOrDefault(x => x.Key == sectionName).Value;
                }
                foreach (var child in allChildrens)
                {
                    return GetConfig(child.Key, sectionName);
                }
            }
            return string.Empty;
        }
    }
}
