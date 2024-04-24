using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Tpf.Common.Enum;
using Tpf.Utils;

namespace Tpf.SqlSugar
{
    public static class SqlSugarExtensions
    {
        public static void AddSqlSugar(this IServiceCollection services)
        {
            if (ConfigHelper.GetMainORMRepository() != RepositoryType.SqlSugar)
            {
                return;
            }

            try
            {
                var dbType = ConfigHelper.GetMainDB();
                var mainConn = ConfigHelper.GetConnectionString(dbType.ToString());

                if (string.IsNullOrEmpty(mainConn))
                {
                    throw new ArgumentNullException($"{dbType} ConnectionString");
                }

                var dbConnections = new Dictionary<DBTypeEnum, string>()
                {
                    { dbType, mainConn }
                };

                // sugar Db连接配置
                var listConfig = new List<ConnectionConfig>();

                foreach (var conn in dbConnections)
                {
                    listConfig.Add(new ConnectionConfig()
                    {
                        ConfigId = conn.Key.ToString(),
                        ConnectionString = conn.Value,
                        DbType = (DbType)conn.Key,
                        IsAutoCloseConnection = true,
                        AopEvents = new AopEvents
                        {
                            OnLogExecuting = (sql, p) =>
                            {
                                // 获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                                ConsoleHelper.WriteColorLine("\n" + UtilMethods.GetSqlString(DbType.PostgreSQL, sql, p), ConsoleColor.Blue);
                            },
                        },
                        MoreSettings = new ConnMoreSettings()
                        {
                            //IsWithNoLockQuery = true,
                            IsAutoRemoveDataCache = true,
                        },
                        // 自定义特性
                        ConfigureExternalServices = new ConfigureExternalServices()
                        {
                            EntityService = (property, column) =>
                            {
                                if (column.IsPrimarykey && property.PropertyType == typeof(int))
                                {
                                    column.IsIdentity = true;
                                }
                            }
                        },
                        InitKeyType = InitKeyType.Attribute
                    });
                }

                // SqlSugarScope是线程安全，可使用单例注入
                services.AddSingleton<ISqlSugarClient>(o => new SqlSugarScope(listConfig));
            }
            catch (Exception)
            {
                //Console.WriteLine(ex);
                throw;
            }
        }
    }
}
