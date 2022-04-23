using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ThatPlatform.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog _log = LogManager.GetLogger(typeof(ExceptionMiddleware));

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null) return;

            _log.Error(e.GetBaseException().ToString());

            await WriteExceptionAsync(context, e).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e is UnauthorizedAccessException)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is Exception)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(e?.Message).ConfigureAwait(false);
        }

    }

    
}
