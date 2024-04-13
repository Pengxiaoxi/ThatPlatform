using System.Linq.Expressions;
using Tpf.Domain.Base.Domain.Entity;
using Tpf.Utils;

namespace Tpf.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity<string>
    {
        public BaseRepository()
        {
            
        }

        public static string ConnectionString
        {
            get
            {
                var dbType = ConfigHelper.GetMainDB();

                return ConfigHelper.GetConnectionString(dbType.ToString());
            }
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> InsertManyAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
