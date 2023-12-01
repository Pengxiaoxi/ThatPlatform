using System.Linq.Expressions;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.Domain.Base.Application.Contacts
{
    public interface IBaseService<T> where T : class
    {
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter);

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

    }
}
