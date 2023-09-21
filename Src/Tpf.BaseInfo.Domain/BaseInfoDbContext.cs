using Microsoft.EntityFrameworkCore;
using Tpf.BaseInfo.Domain.Entity;
using Tpf.ORM.EntityFrameworkCore;
using Tpf.Utils;

namespace Tpf.BaseInfo.Domain
{
    /// <summary>
    /// 1、如需支持主从可考虑提供参数or扩展方法
    /// </summary>
    public class BaseInfoDbContext : TpfDbContextBase
    {
        #region Field

        #endregion

        #region Ctor

        public BaseInfoDbContext(DbContextOptions options
            ) : base(options)
        {

        }
        #endregion

        #region override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var MySqlConnName = "Tpf_Mysql";
            string mysqlDbVersion = "8.0.32";

            optionsBuilder
                .UseMySql(ConfigHelper.GetConnectionString(MySqlConnName), ServerVersion.Parse(mysqlDbVersion))
                .EnableSensitiveDataLogging()
                ;

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("tpf_userinfo");
            modelBuilder.Entity<Dept>().ToTable("tpf_dept");

            base.OnModelCreating(modelBuilder);
        }

        #region DbSets
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Dept> Depts { get; set; }
        #endregion


        #endregion


    }
}
