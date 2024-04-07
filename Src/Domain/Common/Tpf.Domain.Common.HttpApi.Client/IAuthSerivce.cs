using Refit;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.Base.Domain.Context;

namespace Tpf.Domain.Common.HttpApi.Client
{
    public interface IAuthSerivce
    {
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [Get("/userInfo/getCurrentUserInfo")]
        Task<ServiceResult<UserContextInfo>> GetCurrentUserInfo();


    }
}
