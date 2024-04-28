using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using Tpf.Common.Config;
using Tpf.Common.ConfigOptions;
using Tpf.Common.Enum;
using Tpf.Security;

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
            // 根据环境变量加载对应配置
            var path = "appsettings.json";
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrEmpty(env))
            {
                path = $"appsettings.{env}.json";
            }

            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .Add(new JsonConfigurationSource { Path = path, Optional = false, ReloadOnChange = true }) //可以直接读目录里的json文件，修改后自动生效
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

        public static string Get(string[] args)
        {
            return Get(string.Join(':', args));
        }

        public static IConfigurationSection GetSection(string configSectionName)
        {
            if (string.IsNullOrEmpty(configSectionName))
            {
                throw new ArgumentNullException(configSectionName);
            }

            return _configuration.GetSection(configSectionName);
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
                return null;
            }

            var conn = _configuration.GetConnectionString(connName) ?? throw new Exception($"未配置名称为'{connName}'数据库连接字符串，");

            // TODO：Allow Config
            return AESHelper.Decrypt(conn, ConfigHelper.GetSecurityKey16());
        }

        public static string GetMainDBConnectionString()
        {
            var dbType = ConfigHelper.GetMainDB();

            return ConfigHelper.GetConnectionString(dbType.ToString());
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

        public static DBTypeEnum GetMainDB()
        {
            var configMainDB = ConfigHelper.Get(AppConfig.Database_Main);
            
            var mainDB = EnumHelper.GetValue<DBTypeEnum>(configMainDB);

            return mainDB;
        }

        public static string GetSecurityKey16()
        {
            return ConfigHelper.Get(AppConfig.SecurityKey16);
        }

        public static string GetSecurityKey32()
        {
            var result = ConfigHelper.Get(AppConfig.SecurityKey32) ?? "zY3_iJ6[rP0!zS0/uQ3=dW5|lC6*yJ9:";

            return result;
        }

    }

    public static partial class ConfigHelper
    {
        public static T GetOptions<T>() where T : BaseOptions
        {
            var sectionName = Activator.CreateInstance<T>().SectionName;
            var result = _configuration.GetSection(sectionName)?.Get<T>();

            //return result is null 
            //    ? throw new Exception($"{nameof(sectionName)} config section not found.") 
            //    : result;

            return result;
        }

        public static T GetOptions<T>(string sectionName) where T : class
        {
            var result = default(T);

            // Can Get or Bind
            result =  _configuration.GetSection(sectionName)?.Get<T>() ?? default;

            //_configuration.GetSection(optionName).Bind(result);

            return result is null
                ? throw new Exception($"{sectionName} config section not found.")
                : result;
        }

        //public static T GetLiveOptions<T>(string sectionName) where T : class
        //{
        //    var result = default(T);

        //    // Can Get or Bind
        //    result = _configuration.GetSection(sectionName)?.Get<T>() ?? default;

        //    //_configuration.GetSection(sectionName).Bind(result);

        //    return result;
        //}

    }

}
