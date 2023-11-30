using Autofac;
using Tpf.Dapper.Repository;
using Tpf.Domain.AuthInfo.Applciation.Impl;
using Tpf.Domain.AuthInfo.Applciation.Svc;
using Tpf.Domain.AuthInfo.Domain;
using Tpf.EntityFrameworkCore;
using Tpf.IOC;

namespace Tpf.Domain.AuthInfo.HttpApi
{
    internal class DIModule : DependencyInjectionModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var dataAccess = Assembly.GetExecutingAssembly();
            //builder.RegisterAssemblyTypes(dataAccess)
            //    .Where(t => t.Name.StartsWith("Tpf"))
            //    .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(UserService<>)).As(typeof(IUserService<>)).InstancePerDependency();
            
            //builder.RegisterType(typeof(TpfDbContextBase)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(BaseInfoDbContext)).InstancePerLifetimeScope();


            //base.Load(builder);
        }
    }
}
