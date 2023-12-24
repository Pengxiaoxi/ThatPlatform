using Autofac;
using System.Linq.Expressions;
using Tpf.Autofac;
using Tpf.BaseRepository;
using Tpf.Common.Config;
using Tpf.Common.Enum;
using Tpf.Domain.Base.Application.Contacts;
using Tpf.Domain.Base.Domain.Entity;
using Tpf.Utils;

namespace Tpf.Domain.Base.Application
{
    /// <summary>
    /// 1、每个项目都会有一个主数据库，因此对于主数据库的基础操作使用BaseService来完成较为方便
    /// 2、对于副数据库仓储服务，可以在各自Service层通过构造函数注入对应数据库仓储进行使用
    /// </summary>
    /// <typeparam name="T"> Entity class for repository </typeparam>
    /// <typeparam name="TService"> Service class for log </typeparam>
    public class BaseService<T> : IBaseService<T> where T : BaseEntity<string>
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

        public async Task<T> GetAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _repository.GetAsync(whereExpression);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? whereExpression = null)
        {
            return await _repository.GetListAsync(whereExpression);
        }

        public async Task<bool> InsertAsync(T entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public async Task<bool> InsertManyAsync(IEnumerable<T> entities)
        {
            return await _repository.InsertManyAsync(entities);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> UpdateAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateExpression)
        {
            return await _repository.UpdateAsync(whereExpression, updateExpression);
        }

        public async Task<bool> UpdateManyAsync(IEnumerable<T> entities)
        {
            return await _repository.UpdateManyAsync(entities);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await _repository.DeleteAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _repository.DeleteAsync(whereExpression);
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> whereExpression = null)
        {
            return await _repository.CountAsync(whereExpression);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _repository.AnyAsync(whereExpression);
        }


    }
}
