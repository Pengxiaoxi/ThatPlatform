using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Tpf.Middlewares.Swagger;
using Tpf.Common.CoreExtensions.DI;

namespace Tpf.Middlewares
{
    public static class CommonServiceExtensions
    {
        /// <summary>
        /// 服务及中间件注册
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static void AddCommonServiceExtensions(this WebApplicationBuilder builder)
        {
            #region IOC
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // 设置使用Autofac替换IOC容器

            //builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            //{
            //    //containerBuilder.RegisterModule(new AutofacModuleRegister());
            //    //containerBuilder.RegisterBuildCallback(container =>
            //    //{
            //    //    AutofacFactory.SetFCContainer((IContainer)container);
            //    //});
            //}); 
            #endregion

            builder.Services.AddHealthChecks(); // HealthCheck

            builder.Services.AddControllers();

            builder.Services.AddSwaggerMiddleware(); // Swagger

            #region Add Middlewares
            //builder.Services.AddSingleton<AuthorizationMiddleware>();
            builder.Services.AddSingleton<ExceptionMiddleware>();

            //services.AddSingleton<IJobFactory, JobFactory>();
            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。


            #endregion

            #region 接口服务统一注册

            builder.Services.AddModules();

            //.Net Core默认DI示例
            //services.AddTransient(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
            //services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>)); 
            #endregion


            #region Old (TODO: Drop)
            //builder.Services.AddTransient<ITencentCloudDBOperateService, TencentCloudDBOperateService>();

            #endregion

        }

    }

    
}
