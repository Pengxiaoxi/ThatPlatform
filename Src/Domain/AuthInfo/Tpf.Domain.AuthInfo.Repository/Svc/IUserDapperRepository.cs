using Tpf.Dapper.Repository;
using Tpf.Domain.AuthInfo.Domain.Entity;

namespace Tpf.Domain.AuthInfo.Repository.Svc
{
    [Obsolete("Demo")]
    public interface IUserDapperRepository : IDapperRepository<UserInfo>
    {

    }
}
