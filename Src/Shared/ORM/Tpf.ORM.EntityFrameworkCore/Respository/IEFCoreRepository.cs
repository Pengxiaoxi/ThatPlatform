using Tpf.ORM.BaseRepository;

namespace Tpf.ORM.EntityFrameworkCore.Repository
{
    public interface IEFCoreRepository<T> : IBaseRepository<T> where T : class
    {
        
    }
}
