using Tpf.Core.Web.Interface;
using Tpf.Core.Web.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.OpenApi.Models;
using Tpf.Core.ServiceExtension.DI;
using Autofac;
using Quartz;
using Quartz.Impl;
using Tpf.Middleware.Middlewares;

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
            // ָ��Swaggerʹ��Newtonsoft.Json���л�������Swagger�ӿ��ĵ��ڽӿڲ������������JsonProperty������
            services.AddSwaggerGenNewtonsoftSupport();

            // ���appsettings
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppContext.BaseDirectory)
            //    .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true }) //����ֱ�Ӷ�Ŀ¼���json�ļ����޸ĺ��Զ���Ч
            //    .Build();
            //services.AddSingleton<IConfiguration>(configuration);

            // ����ע��BackgroundService����Ŀ�������Զ�����
            //services.AddHostedService<DownloadTaskService>();


            #region DI
            // �м��ע�룬������ͳһע��
            services.AddSingleton<AuthorizationMiddleware>();
            services.AddSingleton<ExceptionMiddleware>();

            //services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//ע��ISchedulerFactory��ʵ����

            // �ӿڷ���ͳһע��
            services.AddModules();


            #region .Net CoreĬ��DIʾ��
            //services.AddTransient(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
            //services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>)); 
            #endregion

            services.AddTransient<ITencentCloudDBOperateService, TencentCloudDBOperateService>();



            #region gRpc Server
            //services.AddGrpc();
            //// ע�������˴������ȵ�Grpc����
            //services.AddCodeFirstGrpc();
            //// ע�����÷���ķ���
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

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseExceptionMiddleware(); // ExceptionMiddleware

            app.UseAuthorizationMiddleware(); // Authorization Middleware

            app.UseAuthorization();

            // �쳣Aop����
            //app.UseExceptionHandlerMidd();

            //app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                #region gRpc Server
                //// TPF Grpc����
                //endpoints.MapGrpcServiceOfTPF();

                //if (!env.IsProduction())
                //{
                //    // ���Grpc��������ս��
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
