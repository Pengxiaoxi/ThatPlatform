using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Tpf.Utils.Extensions.ServiceResult;

namespace Tpf.Middlewares
{
    /// <summary>
    /// ExceptionMiddleware
    /// 
    /// 最近面试被提问：Filter 和 Middleware 哪个好 ？ 
    /// 1、Mvc Filter 可以拿到一些中间件拿不到的数据（？）
    /// 2、Middler 请求管道所有的异常也可以 catch，Filter 做不到（只能catch接口内的）
    /// </summary>
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null) return;

            //_log.Error(e.GetBaseException().ToString());
            await Console.Out.WriteLineAsync(e.GetBaseException().ToString());
            await WriteExceptionAsync(context, e);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e is UnauthorizedAccessException)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is Exception)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(ServiceResult.IsFailed(e?.Message))); // 此处避免引用其他组件，可更换为拼接的Json字符串
            //await context.Response.WriteAsync(e?.Message).ConfigureAwait(false);
        }


    }

    /// <summary>
    /// ExceptionMiddlewareExtensions
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// UseExceptionMiddleware
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }

}
