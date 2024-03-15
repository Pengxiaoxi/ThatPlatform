using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using Tpf.Utils;

namespace Tpf.EntityFrameworkCore
{
    public class TpfDbContextBase : DbContext
    {
        public TpfDbContextBase()
        {

        }

        protected TpfDbContextBase(DbContextOptions options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string mysqlDbVersion = "8.0.32";

            optionsBuilder
                .UseMySql(ConfigHelper.GetMainDBConnectionString(), ServerVersion.Parse(mysqlDbVersion))
                .EnableSensitiveDataLogging()
                ;

            // Log
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
