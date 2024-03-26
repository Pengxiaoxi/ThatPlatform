using System.Threading.Tasks;
using Tpf.BaseRepository;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.EntityFrameworkCore.Repository
{
    public interface IEFCoreRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity<string>
    {
        Task<bool> SaveChangesAsync();

    }
}
