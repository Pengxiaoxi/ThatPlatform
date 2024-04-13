using System;
using System.Threading.Tasks;
using Tpf.BaseRepository;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.EntityFrameworkCore.Repository
{
    [Obsolete("推荐使用原生 using new DbContext()")]
    public interface IEFCoreRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity<string>
    {
        Task<bool> SaveChangesAsync();

    }
}
