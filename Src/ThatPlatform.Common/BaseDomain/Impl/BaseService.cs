using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThatPlatform.Common.BaseDomain.Entity;
using ThatPlatform.Common.BaseDomain.Svc;
using ThatPlatform.Common.BaseORM.MongoDB;
using ThatPlatform.Logging;

namespace ThatPlatform.Common.BaseDomain.Impl
{
    /// <summary>
    /// 1、每个项目都会有一个主数据库，因此对于主数据库的基础操作使用BaseService来完成较为方便
    /// 2、对于副数据库仓储服务，可以在各自Service层通过构造函数注入对应数据库仓储进行使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : IBaseService<T> where T: BaseEntity<string>
    {
        #region Field
        protected readonly ILogging _logger;
        protected readonly IMongoDBRepository<T> _repository;
        #endregion

        #region Ctor
        public BaseService(ILogging logger
            , IMongoDBRepository<T> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion

        #region Public Method
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter)
        {
            var result = await _repository.FindAsync(filter);
            _logger.Info(JsonConvert.SerializeObject(result));
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
