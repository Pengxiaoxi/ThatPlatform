using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector.Logging;
using System.Configuration;
using Tpf.BaseInfo.Domain.Entity;
using Tpf.ORM.EntityFrameworkCore;

namespace Tpf.BaseInfo.Domain
{
    public class BaseInfoDbContext : TpfDbContextBase
    {
        #region Field
        private const string connectioNname = "tpf_mysql";
        private const string mysqlDbVersion = "8.0.32";

        private readonly ILoggerFactory _loggerFactory;
        #endregion

        #region DbSets
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Dept> Depts { get; set; }
        #endregion

        #region Ctor
        public BaseInfoDbContext()
        {
            
        }

        public BaseInfoDbContext(DbContextOptions options
            ) : base(options)
        {
            //_loggerFactory = new LoggerFactory(new[] {
            //  new ConsoleLoggerProvider((_, __) => true, true)
        }
        #endregion

        #region override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=127.0.0.1;port=3306;user=root;password=123456;database=tpf;";
            optionsBuilder
                //.UseMySql(ConfigurationManager.AppSettings.Get($"ConnectionStrings:{connectioNname}").ToString(), ServerVersion.Parse(mysqlDbVersion))
                .UseMySql(connectionString, ServerVersion.Parse(mysqlDbVersion))
                //.UseLoggerFactory(_loggerFactory)
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


        
        #endregion


    }
}
