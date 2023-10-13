using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.ORM.SqlSugar
{
    public class TestClient
    {
        public static SqlSugarClient GetInstance()
        {
            var conn = "PORT=37872;DATABASE=project_modify_base;HOST=10.8.0.5;PASSWORD=1Zzx52dos9q;USER ID=postgres;MaxPoolSize=512;";

            return new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.PostgreSQL,
                ConnectionString = conn,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
                AopEvents = new AopEvents
                {
                    OnLogExecuting = (sql, p) =>
                    {
                        //Console.WriteLine(sql);
                        //Console.WriteLine(string.Join(",", p?.Select(it => it.ParameterName + ":" + it.Value)));

                        //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                        Console.WriteLine("\n" + UtilMethods.GetSqlString(DbType.PostgreSQL, sql, p));
                    }
                }
            });
        }
    }
}
