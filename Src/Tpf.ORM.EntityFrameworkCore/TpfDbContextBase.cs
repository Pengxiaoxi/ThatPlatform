using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Tpf.ORM.EntityFrameworkCore
{
    public class TpfDbContextBase : DbContext
    {
        public TpfDbContextBase()
        {
            
        }

        protected TpfDbContextBase(DbContextOptions options) : base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseMySql(this.connectionString, ServerVersion.);
        //    optionsBuilder.UseLoggerFactory(_loggerFactory);
        //    base.OnConfiguring(optionsBuilder);
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
