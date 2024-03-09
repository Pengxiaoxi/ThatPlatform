using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;

namespace Tpf.Authentication.Jwt
{
    public class AppJwtBearerEvents : JwtBearerEvents
    {
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

        public override Task TokenValidated(TokenValidatedContext context)
        {
            return Task.CompletedTask;
        }

        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            Console.WriteLine($"Exception: {context.Exception}");

            return Task.CompletedTask;
        }

        public override Task Challenge(JwtBearerChallengeContext context)
        {
            Console.WriteLine($"Authenticate Failure: {context.AuthenticateFailure}");
            Console.WriteLine($"Error: {context.Error}");
            Console.WriteLine($"Error Description: {context.ErrorDescription}");
            Console.WriteLine($"Error Uri: {context.ErrorUri}");

            return Task.CompletedTask;
        }

        public override Task Forbidden(ForbiddenContext context)
        {
            return Task.CompletedTask;
        }
    }
}
