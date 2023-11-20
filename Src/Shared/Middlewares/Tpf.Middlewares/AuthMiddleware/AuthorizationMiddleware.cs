using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Tpf.Middlewares
{
    /// <summary>
    /// AuthorizationMiddleware [Need Config]
    /// </summary>
    public class AuthorizationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //var token = context.Request.Headers["token"].ToString();
            //if (string.IsNullOrWhiteSpace(token))
            //{
            //    throw new Exception("Access failed !");
            //}

            await next(context);
        }
    }

    /// <summary>
    /// AuthorizationMiddlewareExtension
    /// </summary>
    public static class AuthorizationMiddlewareExtensions
    {
        /// <summary>
        /// UseAuthorizationMiddleware
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
