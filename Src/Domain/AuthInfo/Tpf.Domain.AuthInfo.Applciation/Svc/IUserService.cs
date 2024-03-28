using System.Collections.Generic;
using System.Threading.Tasks;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.Base.Application.Svc;
using Tpf.Domain.Base.Domain.Context;

namespace Tpf.Domain.AuthInfo.Applciation.Svc
{
    public interface IUserService : IBaseService<UserInfo>
    {
        /// <summary>
        /// GetOrgByUserByGrpc
        /// </summary>
        /// <returns></returns>
        Task<object> GetOrgByUserByGrpc();

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        Task<List<UserInfoOutputDto>> GetUserInfoList();

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserContextInfo> GetCurrentUserInfo();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddUser(UserInfo model);

    }
}
