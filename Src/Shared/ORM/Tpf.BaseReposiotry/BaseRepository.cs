using System.Linq.Expressions;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity<string>
    {
        public BaseRepository()
        {
            
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>>? whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>>? whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertManyAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateManyAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
