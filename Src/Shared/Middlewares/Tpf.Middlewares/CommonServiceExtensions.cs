using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
//using Quartz;
//using Quartz.Impl;
using Tpf.Middlewares.Swagger;
//using Tpf.Common.CoreExtensions.DI;
using Autofac;
using Tpf.Autofac;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

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
            // 设置使用Autofac替换IOC容器
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(AutofacFactory.Register);

            builder.Host.ConfigureContainer<ContainerBuilder>((hostBuilder, containerBuilder) =>
            {
                var autofacModuleType = typeof(AutofacModule);
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var modules = assemblies.SelectMany(x => x.GetTypes())
                    .Where(x => x.IsAssignableFrom(autofacModuleType) && x != autofacModuleType && x.IsClass && !x.IsAbstract)
                    .ToList();

                foreach (Type type in modules)
                {
                    var module = Activator.CreateInstance(type, null) as AutofacModule;
                    if (module != null)
                    {
                        containerBuilder.RegisterModule(module);
                    }
                    
                }

                //containerBuilder.RegisterModule(new AutofacModule());
                containerBuilder.RegisterBuildCallback(container =>
                {
                    AutofacFactory.SetFCContainer((IContainer)container);
                });
            });

            #endregion

            builder.Services.AddHealthChecks(); // HealthCheck

            builder.Services.AddControllers();

            builder.Services.AddSwaggerMiddleware(); // Swagger

            #region Add Middlewares
            //builder.Services.AddSingleton<AuthorizationMiddleware>();
            builder.Services.AddSingleton<ExceptionMiddleware>();

            //services.AddSingleton<IJobFactory, JobFactory>();
            //builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。


            #endregion

            #region 接口服务统一注册

            //builder.Services.AddModules();

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
