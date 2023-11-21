using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using System;

namespace Tpf.Middlewares.Swagger
{
    public static class SwaggerMiddleware
    {

        public static void AddSwaggerMiddleware(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tpf", Version = "v1" });

                c.AddServer(new OpenApiServer()
                {
                    Url = "http://localhost:8000",
                    Description = "Tpf"
                });

                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Tpf.Core.Api.xml"), true);
            });

            // 指定Swagger使用Newtonsoft.Json序列化【避免Swagger接口文档内接口参数与对象属性JsonProperty不符】
            services.AddSwaggerGenNewtonsoftSupport();
        }


        public static void UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tpf v1");
            });
        }

        /// <summary>
        /// UseKnife4UI
        /// https://doc.xiaominfo.com/docs/action/dotnetcore-knife4j-guid
        /// </summary>
        /// <param name="app"></param>
        public static void UseKnife4UI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseKnife4UI(c =>
            {
                c.RoutePrefix = ""; // serve the UI at root
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
            });

            app.MapSwagger("{documentName}/api-docs");
        }
    }
}
