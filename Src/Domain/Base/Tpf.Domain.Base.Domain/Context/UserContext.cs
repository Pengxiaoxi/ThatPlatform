using Autofac;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Tpf.Autofac;

namespace Tpf.Domain.Base.Domain.Context
{
    /// <summary>
    /// 用户上下文
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// 当前登陆者账号
        /// </summary>
        public static string? CurrentUserAccount => GetCurrentUserAccount();

        /// <summary>
        /// 当前登陆者账号
        /// </summary>
        public static string? CurrentUserName => GetCurrentUserName();

        public static HttpContext? HttpContext => AutofacFactory.GetContainer().Resolve<IHttpContextAccessor>()?.HttpContext;


        #region Private Method
        private static string? GetCurrentUserAccount()
        {
            // TODO: UserContext

            //return HttpContext?.User?.Claims?.FirstOrDefault(t => t.Type.Equals(JwtClaimTypes.Id))?.Value;
            
            return HttpContext?.User?.Identity?.Name;
        }

        private static string? GetCurrentUserName()
        {
            return HttpContext?.User?.Claims?.FirstOrDefault(t => t.Type.Equals(JwtClaimTypes.Name))?.Value;
        }
        #endregion


    }
}
