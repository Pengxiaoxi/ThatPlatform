using Tpf.BaseRepository;

namespace Tpf.EntityFrameworkCore.Repository
{
    public interface IEFCoreRepository<T> : IBaseService<T> where T : class
    {

    }
}
