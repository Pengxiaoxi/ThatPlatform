using Tpf.Core.Web.Interface;
using Tpf.Core.Web.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.OpenApi.Models;
using Tpf.Core.ServiceExtension.DI;
using Autofac;
using Tpf.Core.CoreExtensions.HostBuilderExtensions;
using Quartz.Spi;
using Tpf.Jobs.QuartzNet;
using Quartz;
using Quartz.Impl;
using ProtoBuf.Grpc.Server;
using Tpf.Grpc.Server;

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
            });

            // 添加appsettings
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppContext.BaseDirectory)
            //    .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true }) //可以直接读目录里的json文件，修改后自动生效
            //    .Build();
            //services.AddSingleton<IConfiguration>(configuration);

            // 服务注册BackgroundService，项目启动则自动启动
            //services.AddHostedService<DownloadTaskService>();

            #region DI
            //services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。

            // 接口服务统一注册
            services.AddModules();


            #region .Net Core默认DI示例
            //services.AddTransient(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
            //services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>)); 
            #endregion

            services.AddTransient<ITencentCloudDBOperateService, TencentCloudDBOperateService>();

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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tpf v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // 异常Aop处理
            app.UseExceptionHandlerMidd();

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
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.ModuleRegister();
        }
    }
}
