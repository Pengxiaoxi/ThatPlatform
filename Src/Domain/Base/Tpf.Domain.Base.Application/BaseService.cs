using Autofac;
using System.Linq.Expressions;
using Tpf.Autofac;
using Tpf.BaseRepository;
using Tpf.Common.Config;
using Tpf.Common.Enum;
using Tpf.Domain.Base.Application.Contacts;
using Tpf.Utils;

namespace Tpf.Domain.Base.Application
{
    /// <summary>
    /// 1、每个项目都会有一个主数据库，因此对于主数据库的基础操作使用BaseService来完成较为方便
    /// 2、对于副数据库仓储服务，可以在各自Service层通过构造函数注入对应数据库仓储进行使用
    /// </summary>
    /// <typeparam name="T"> Entity class for repository </typeparam>
    /// <typeparam name="TService"> Service class for log </typeparam>
    public class BaseService<T> : IBaseService<T> where T : class
    {
        #region Field
        protected readonly IBaseRepository<T> _repository;

        private static readonly RepositoryType Main_RepositoryType = ConfigHelper.GetMainORMRepository();

        #endregion

        #region Ctor
        /// <summary>
        /// TODO：根据配置需要使用的ORM来条件注册
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public BaseService()
        {
            _repository = AutofacFactory.GetContainer().ResolveKeyed<IBaseRepository<T>>(Main_RepositoryType);
        }
        #endregion

        #region Public Method
        public virtual async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
        {
            var result = await _repository.GetAsync(filter);
            return result;
        }

        public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null)
        {
            var result = await _repository.GetListAsync(filter);
            return result;
        }

        public virtual async Task InsertAsync(T entity)
        {
            await _repository.Insert(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _repository.Update(entity);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }
        #endregion


    }
}
