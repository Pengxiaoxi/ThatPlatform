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

            // 添加appsettings
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true }) //可以直接读目录里的json文件，修改后自动生效
                .Build();
            services.AddSingleton<IConfiguration>(configuration);

            // 服务注册BackgroundService，项目启动则自动启动
            services.AddHostedService<DownloadTaskService>();

            #region DI
            // 统一依赖注入
            services.AddModules();

            // [Drop]
            // 直接注入泛型 IRepository<> 与 IBaseService<> 接口（IBaseService<> 接口注入但子类未注入，故此方案不可行）
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
