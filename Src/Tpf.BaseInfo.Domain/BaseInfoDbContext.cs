using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Tpf.BaseInfo.Domain.Entity;
using Tpf.ORM.EntityFrameworkCore;

namespace Tpf.BaseInfo.Domain
{
    public class BaseInfoDbContext : TpfDbContextBase
    {
        #region Field

        #endregion

        #region DbSets
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Dept> Depts { get; set; }
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
            //optionsBuilder
            //    //.UseMySql(connectionString, ServerVersion.Parse(mysqlDbVersion))
            //    .EnableSensitiveDataLogging()
            //    ;

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("tpf_userinfo");
            modelBuilder.Entity<Dept>().ToTable("tpf_dept");

            base.OnModelCreating(modelBuilder);
        } 


        
        #endregion


    }
}
