using Autofac;
using Tpf.Autofac;
using Tpf.BaseRepository;
using Tpf.Common.Enum;
using Tpf.Dapper.Repository;
using Tpf.Domain.Base.Application;
using Tpf.Domain.Base.Application.Contacts;
using Tpf.EntityFrameworkCore.Repository;
using Tpf.MongoDB.Respository;
using Tpf.SqlSugar.Respository;

namespace Tpf.Domain.Base.HttpApi
{
    public class DomainRegisterModule : AutofacRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region BaseRepository|BaseSerivce|ILogger
            // 注册基础仓储和基础服务
            //RepositoryType[] EnableRepositoryType = [RepositoryType.SqlSugar, RepositoryType.Mongo];

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EFCoreRepository<>)).Keyed(RepositoryType.EFCore, typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(SqlSugerRepository<>)).Keyed(RepositoryType.SqlSugar, typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DapperRepository<>)).Keyed(RepositoryType.Dapper, typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(MongoDBRepository<>)).Keyed(RepositoryType.Mongo, typeof(IBaseRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EFCoreRepository<>)).As(typeof(IEFCoreRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(SqlSugerRepository<>)).As(typeof(ISqlSugerRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DapperRepository<>)).As(typeof(IDapperRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(MongoDBRepository<>)).As(typeof(IMongoDBRepository<>)).InstancePerLifetimeScope();


            #endregion

            #region Your Service
            // Your Service

            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerLifetimeScope();


            #endregion

        }
    }
}
