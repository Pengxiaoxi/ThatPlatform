using Tpf.Core.Web.Interface;
using Tpf.Core.Web.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.OpenApi.Models;
using Tpf.Common.ServiceExtension.DI;
using Autofac;
using Quartz;
using Quartz.Impl;
using Tpf.Middleware.Middlewares;
using Tpf.BaseInfo.Domain;
using Tpf.ORM.Dapper;
using Microsoft.AspNetCore.Mvc.Controllers;
using IGeekFan.AspNetCore.Knife4jUI;

namespace Tpf.Core.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson((builder) => 
                {

                })
                ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tpf", Version = "v1" });
                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });
            });
            // 指定Swagger使用Newtonsoft.Json序列化【避免Swagger接口文档内接口参数与对象属性JsonProperty不符】
            services.AddSwaggerGenNewtonsoftSupport();

            // 添加appsettings
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppContext.BaseDirectory)
            //    .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true }) //可以直接读目录里的json文件，修改后自动生效
            //    .Build();
            //services.AddSingleton<IConfiguration>(configuration);

            // 服务注册BackgroundService，项目启动则自动启动
            //services.AddHostedService<DownloadTaskService>();


            #region DI
            // 中间件注入，后续需统一注入
            services.AddSingleton<AuthorizationMiddleware>();
            services.AddSingleton<ExceptionMiddleware>();

            //services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。

            #region EF Core DbContext + Mysql
            services.AddDbContext<BaseInfoDbContext>(); 

            #endregion

            // 接口服务统一注册
            services.AddModules();
            

            #region .Net Core默认DI示例
            //services.AddTransient(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
            //services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>)); 
            #endregion

            services.AddTransient<ITencentCloudDBOperateService, TencentCloudDBOperateService>();

            // Dapper Repository
            services.AddTpfDapper();

            #region gRpc Server
            //services.AddGrpc();
            //// 注册启用了代码优先的Grpc服务
            //services.AddCodeFirstGrpc();
            //// 注册启用反射的服务
            //services.AddGrpcReflectionOfTPF();
            #endregion

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            Console.WriteLine($"EnvironmentName: {env.EnvironmentName}");


            #region Swagger Knife4UI
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tpf v1"));

            app.UseKnife4UI(c =>
            {
                c.RoutePrefix = ""; // serve the UI at root
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
            });

            #endregion

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseExceptionMiddleware(); // ExceptionMiddleware

            app.UseAuthorizationMiddleware(); // Authorization Middleware

            app.UseAuthorization();

            // 异常Aop处理
            //app.UseExceptionHandlerMidd();

            app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                #region gRpc Server
                //// TPF Grpc服务
                //endpoints.MapGrpcServiceOfTPF();

                //if (!env.IsProduction())
                //{
                //    // 添加Grpc反射服务终结点
                //    endpoints.MapGrpcReflectionService();
                //} 
                #endregion

                endpoints.MapSwagger("{documentName}/api-docs");
            });

            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.ModuleRegister();
        }
    }
}
