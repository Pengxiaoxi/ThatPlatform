using Tpf.BaseRepository;

namespace Tpf.SqlSugar.Respository
{
    public interface ISqlSugerRepository<T> : IBaseService<T> where T : class
    {

    }
}
