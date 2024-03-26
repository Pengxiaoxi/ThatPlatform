using Microsoft.EntityFrameworkCore;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.EntityFrameworkCore;

namespace Tpf.Domain.AuthInfo.Domain
{
    /// <summary>
    /// 1、如需支持主从可考虑提供参数or扩展方法
    /// </summary>
    public class AuthInfoDbContext : TpfDbContextBase
    {
        #region Field

        #endregion

        #region Ctor
        public AuthInfoDbContext()
        {

        }

        public AuthInfoDbContext(DbContextOptions options
            ) : base(options)
        {

        }
        #endregion

        #region override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("base_user");
            modelBuilder.Entity<Dept>().ToTable("tpf_dept");

            #region TODO: 批量添加实体映射
            //var assemblies = GetCurrentPathAssembly();
            //foreach (var assembly in assemblies)
            //{
            //    var entityTypes = assembly.GetTypes()
            //        .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
            //        .Where(type => type.IsClass)
            //        .Where(type => type.BaseType != null)
            //        .Where(type => typeof(IEntity).IsAssignableFrom(type));

            //    foreach (var entityType in entityTypes)
            //    {
            //        if (modelBuilder.Model.FindEntityType(entityType) != null)
            //            continue;
            //        modelBuilder.Model.AddEntityType(entityType);
            //    }
            //} 
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #region DbSets
        //public DbSet<UserInfo> UserInfos { get; set; }
        //public DbSet<Dept> Depts { get; set; }
        #endregion


        #endregion


    }
}
