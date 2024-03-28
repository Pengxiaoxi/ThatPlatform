using Tpf.BaseRepository;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.SqlSugar.Respository
{
    public interface ISqlSugerRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity<string>
    {

    }
}
