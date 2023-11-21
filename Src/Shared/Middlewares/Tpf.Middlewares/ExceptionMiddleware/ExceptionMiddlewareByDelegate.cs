using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Tpf.Utils.Extensions.ServiceResult;

namespace Tpf.Middlewares
{
    public class ExceptionMiddlewareByDelegate
    {
        private readonly RequestDelegate _next;
        //private static readonly ILog _log = LogManager.GetLogger(typeof(ExceptionMiddlewareByDelegate));

        public ExceptionMiddlewareByDelegate(RequestDelegate next)
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

            //_log.Error(e.GetBaseException().ToString());

            await WriteExceptionAsync(context, e);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e is UnauthorizedAccessException)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is Exception)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json";


            await context.Response.WriteAsync(JsonConvert.SerializeObject(ServiceResult.IsFailed(e?.Message)));
            //await context.Response.WriteAsync(e?.Message).ConfigureAwait(false);
        }

    }


}
