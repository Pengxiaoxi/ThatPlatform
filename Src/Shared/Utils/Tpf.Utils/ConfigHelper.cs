using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.Common.Config;
using Tpf.Common.Enum;

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
        /// Get
        /// </summary>
        /// <param name="configSectionName"></param>
        /// <returns></returns>
        public static string Get(string configSectionName)
        {
            if (string.IsNullOrEmpty(configSectionName))
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

        public static string GetConnectionString(string connName)
        {
            if (string.IsNullOrEmpty(connName))
            {
                return string.Empty;
            }

            return _configuration.GetConnectionString(connName);
        }

        public static RepositoryType GetMainORM()
        {
            // 默认 Dapper
            var mainORM = default(RepositoryType);

            var configMainORM = ConfigHelper.Get(AppConfig.ORM_MainORM);
            if (!string.IsNullOrEmpty(configMainORM)) 
            {
                Enum.TryParse<RepositoryType>(configMainORM, out mainORM);
            }

            return mainORM;
        }
    }
}
