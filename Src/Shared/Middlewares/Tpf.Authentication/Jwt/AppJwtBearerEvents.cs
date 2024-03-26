using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Tpf.Utils;

namespace Tpf.Authentication.Jwt
{
    public class AppJwtBearerEvents : JwtBearerEvents
    {
        /// <summary>
        /// 当收到请求时回调，注意，此时还未获取到token。我们可以在该方法内自定义token的获取方式，然后将获取到的token赋值到context.Token（不包含Scheme）。
        /// 只要我们取到的token既非Null也非Empty，那后续验证就会使用该token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task MessageReceived(MessageReceivedContext context)
        {
            // 从 Http Request Header 中获取 Authorization
            string authorization = context.Request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(authorization))
            {
                context.NoResult();
                return Task.CompletedTask;
            }

            // 必须为 Bearer 认证方案
            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                // 赋值token
                context.Token = authorization["Bearer ".Length..].Trim();
            }

            if (string.IsNullOrEmpty(context.Token))
            {
                context.NoResult();
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// token验证通过后回调
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenValidated(TokenValidatedContext context)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 由于认证过程中抛出异常，导致身份认证失败后回调
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            Console.WriteLine($"Exception: {context.Exception}");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 质询时回调
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Challenge(JwtBearerChallengeContext context)
        {
            Console.WriteLine($"Authenticate Failure: {context.AuthenticateFailure}");
            Console.WriteLine($"Error: {context.Error}");
            Console.WriteLine($"Error Description: {context.ErrorDescription}");
            Console.WriteLine($"Error Uri: {context.ErrorUri}");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 当出现403（Forbidden，禁止）时回调
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Forbidden(ForbiddenContext context)
        {
            return Task.CompletedTask;
        }
    }
}
