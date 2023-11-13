using Tpf.ORM.BaseRepository;

namespace Tpf.ORM.SqlSugar.Respository
{
    public interface ISqlSugerRepository<T> : IBaseRepository<T> where T : class
    {

    }
}
