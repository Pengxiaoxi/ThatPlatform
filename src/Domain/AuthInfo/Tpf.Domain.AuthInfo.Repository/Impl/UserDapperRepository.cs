using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using Tpf.Dapper.Repository;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.AuthInfo.Repository.Svc;

namespace Tpf.Domain.AuthInfo.Repository
{

    public class UserDapperRepository : DapperRepository<UserInfo>, IUserDapperRepository
    {
        public UserDapperRepository(IConfiguration config) : base(config)
        {

        }

        public override Task<UserInfo> GetAsync(Expression<Func<UserInfo, bool>> expression)
        {
            return base.GetAsync(expression);
        }
    }
}
