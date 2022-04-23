using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tpf.Common.BaseDomain.Entity;

namespace Tpf.Common.BaseDomain.Svc
{
    public interface IBaseService<T> where T : BaseEntity<string>
    {
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter);

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

    }
}
