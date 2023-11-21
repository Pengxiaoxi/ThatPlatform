using System.Collections.Generic;
using System.Threading.Tasks;
using Tpf.Domain.Base.Application.Contacts;
using Tpf.Domain.Base.Domain.Entity;
using Tpf.Domain.UserInfo.Applciation.Dto;

namespace Tpf.Domain.UserInfo.Applciation.Svc
{
    public interface IUserService<T> : IBaseService<T> where T : BaseEntity<string>
    {
        /// <summary>
        /// GetOrgByUserByGrpc
        /// </summary>
        /// <returns></returns>
        Task<object> GetOrgByUserByGrpc();

        Task<List<UserInfoOutputDto>> GetUserInfoList();


    }
}
