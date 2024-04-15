using Refit;
using Tpf.Autofac;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.Base.Domain.Context;

namespace Tpf.Domain.Common.RestApplication
{
    [NotRegister]
    public interface IAuthRestSerivce
    {
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [Get("/userInfo/getCurrentUserInfo")]
        Task<Result<UserContextInfo>> GetCurrentUserInfo();


    }
}
