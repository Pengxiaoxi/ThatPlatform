using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;

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
            //optionsBuilder.LogTo(message => Debug.WriteLine(message));
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Trace);
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
