using System.Collections.Generic;
using System.Threading.Tasks;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.Base.Application.Contacts;

namespace Tpf.Domain.AuthInfo.Applciation.Svc
{
    public interface IUserService : IBaseService<UserInfo>
    {
        /// <summary>
        /// GetOrgByUserByGrpc
        /// </summary>
        /// <returns></returns>
        Task<object> GetOrgByUserByGrpc();

        Task<List<UserInfoOutputDto>> GetUserInfoList();


    }
}
