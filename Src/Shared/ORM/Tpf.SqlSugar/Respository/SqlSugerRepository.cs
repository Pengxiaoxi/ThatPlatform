using IdentityModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tpf.BaseRepository;
using Tpf.Domain.Base.Domain.Entity;
using Tpf.SqlSugar.Uow;

namespace Tpf.SqlSugar.Respository
{
    public class SqlSugerRepository<TEntity> : BaseRepository<TEntity>, ISqlSugerRepository<TEntity> where TEntity : BaseEntity<string>, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqlSugarScope _dbBase;

        /// <summary>
        /// Ctor
        /// </summary>
        public SqlSugerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbBase = unitOfWork.GetDbClient();
        }

        private ISqlSugarClient _db
        {
            get
            {
                return _dbBase;
            }
        }

        #region Publich Method
        public override async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _db.Queryable<TEntity>().FirstAsync(expression);
        }

        public override async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return await _db.Queryable<TEntity>()
                .WhereIF(expression != null, expression)
                .ToListAsync();
        }

        public override async Task<bool> InsertAsync(TEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
        }

        public override async Task<bool> InsertManyAsync(IEnumerable<TEntity> entities)
        {
            return await _db.Insertable(entities.ToList()).ExecuteCommandAsync() > 0;
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        public override async Task<bool> UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            return await _db.Updateable(entities.ToList()).ExecuteCommandHasChangeAsync();
        }

        public override async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            return await _db.Updateable<TEntity>()
                .SetColumns(updateExpression)
                .WhereIF(whereExpression != null, whereExpression)
                .ExecuteCommandHasChangeAsync();
        }

        public override async Task<bool> DeleteAsync(TEntity entity)
        {
            

            return await this.DeleteByIdAsync(entity.Id);
        }

        public override async Task<bool> DeleteByIdAsync(string id)
        {
            Expression<Func<TEntity, bool>> whereExpression = x => x.Id == id;

            return await this.DeleteAsync(whereExpression);
        }

        public override async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Deleteable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .ExecuteCommandHasChangeAsync();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            return await _db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .CountAsync();
        }

        public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .AnyAsync();
        }

        #endregion
    }
}
