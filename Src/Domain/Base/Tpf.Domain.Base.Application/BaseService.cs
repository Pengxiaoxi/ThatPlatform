using Autofac;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Tpf.Autofac;
using Tpf.BaseRepository;
using Tpf.Common.Enum;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.Domain.Base.Application
{
    /// <summary>
    /// 1、每个项目都会有一个主数据库，因此对于主数据库的基础操作使用BaseService来完成较为方便
    /// 2、对于副数据库仓储服务，可以在各自Service层通过构造函数注入对应数据库仓储进行使用
    /// </summary>
    /// <typeparam name="T"> Entity class for repository </typeparam>
    /// <typeparam name="TService"> Service class for log </typeparam>
    public class BaseService<T> : Contacts.IBaseService<T>
        where T : BaseEntity<string>
    {
        #region Field
        protected readonly ILogger<BaseService<T>> _log;
        protected readonly IBaseRepository<T> _repository;

        private int MAIN_REPOSITORY = RepositoryType.MySqlRepository.GetHashCode();
        #endregion

        #region Ctor
        /// <summary>
        /// TODO：根据配置需要使用的ORM来条件注册
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public BaseService(ILogger<BaseService<T>> log
            //, [FromKeyedServices(MAIN_REPOSITORY)] IBaseRepository<T> repository
            , IBaseRepository<T> repository
            )
        {
            _log = log;
            //_repository = repository;
            _repository = AutofacFactory.GetContainer().ResolveKeyed<IBaseRepository<T>>(MAIN_REPOSITORY);
        }
        #endregion

        #region Public Method
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
        {
            var result = await _repository.GetAsync(filter);
            return result;
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter)
        {
            var result = await _repository.GetListAsync(filter);
            //_log.Info(JsonConvert.SerializeObject(result));
            return result;
        }

        public async Task InsertAsync(T entity)
        {
            await _repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }
        #endregion


    }
}
