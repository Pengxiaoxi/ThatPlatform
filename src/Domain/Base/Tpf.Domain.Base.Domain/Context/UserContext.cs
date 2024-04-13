using Autofac;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Tpf.Autofac;

namespace Tpf.Domain.Base.Domain.Context
{
    /// <summary>
    /// 用户上下文
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// 当前登录者账号
        /// </summary>
        public static string? CurrentUserAccount => GetCurrentUserAccount();

        /// <summary>
        /// 当前登录者账号
        /// </summary>
        public static string? CurrentUserName => GetCurrentUserName();

        /// <summary>
        /// Token
        /// </summary>
        public static string? Token => GetToken();


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

        private static string? GetToken()
        {
            if (HttpContext?.Request?.Headers != null && HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return HttpContext?.Request?.Headers[HeaderNames.Authorization];
            }

            throw new ArgumentNullException(HeaderNames.Authorization);
        }

        #endregion


    }
}
