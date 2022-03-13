using ThatPlatform.Core.Web.Interface;
using ThatPlatform.Core.Web.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.OpenApi.Models;
using ThatPlatform.BaseInfo.Applciation.Impl;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.Common.BaseORM.MongoDB;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.Infrastructure.ServiceExtension.DI;
using ThatPlatform.Common.BaseDomain.Svc;
using ThatPlatform.Common.BaseDomain.Impl;

namespace ThatPlatform.Core.Web
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ThatPlatform", Version = "v1" });
            });

            // ���appsettings
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true }) //����ֱ�Ӷ�Ŀ¼���json�ļ����޸ĺ��Զ���Ч
                .Build();
            services.AddSingleton<IConfiguration>(configuration);

            // ����ע��BackgroundService����Ŀ�������Զ�����
            services.AddHostedService<DownloadTaskService>();

            #region DI
            // ͳһ����ע��
            services.AddModules();

            // [Drop]
            // ֱ��ע�뷺�� IRepository<> �� IBaseService<> �ӿڣ�IBaseService<> �ӿ�ע�뵫����δע�룬�ʴ˷��������У�
            //services.AddTransient(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
            //services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));

            services.AddTransient<ITencentCloudDBOperateService, TencentCloudDBOperateService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThatPlatform v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
