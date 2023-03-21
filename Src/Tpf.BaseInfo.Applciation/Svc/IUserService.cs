using System.Collections.Generic;
using System.Threading.Tasks;
using Tpf.BaseInfo.Domain.Entity;
using Tpf.Common.BaseDomain.Entity;
using Tpf.Common.BaseDomain.Svc;

namespace Tpf.BaseInfo.Applciation.Svc
{
    public interface IUserService<T> : IBaseService<T> where T : BaseEntity<string>
    {
        /// <summary>
        /// GetOrgByUserByGrpc
        /// </summary>
        /// <returns></returns>
        Task<object> GetOrgByUserByGrpc();

        Task<List<UserInfo>> GetUserInfoList();
    }
}
