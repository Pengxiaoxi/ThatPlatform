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
    /// <summary>
    /// 基础获取配置方法
    /// </summary>
    public static partial class ConfigHelper
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

        
    }

    /// <summary>
    /// 获取配置的业务参数方法
    /// </summary>
    public static partial class ConfigHelper
    {
        public static string GetConnectionString(string connName)
        {
            if (string.IsNullOrEmpty(connName))
            {
                return string.Empty;
            }

            return _configuration.GetConnectionString(connName);
        }

        /// <summary>
        /// 获取配置的默认 ORM 类型
        /// </summary>
        /// <returns></returns>
        public static RepositoryType GetMainORMRepository()
        {
            // 默认 Dapper
            var mainORM = default(RepositoryType);

            var configMainORM = ConfigHelper.Get(AppConfig.ORM_Main);
            if (!string.IsNullOrEmpty(configMainORM))
            {
                Enum.TryParse<RepositoryType>(configMainORM, out mainORM);
            }

            return mainORM;
        }

        public static DBType GetMainDB()
        {
            var mainDB = default(DBType);

            var configMainDB = ConfigHelper.Get(AppConfig.Database_Main);
            if (!string.IsNullOrEmpty(configMainDB))
            {
                Enum.TryParse<DBType>(configMainDB, out mainDB);
            }

            return mainDB;
        }
    }
}
