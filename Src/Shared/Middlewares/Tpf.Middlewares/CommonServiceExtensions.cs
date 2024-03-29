﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
//using Quartz;
//using Quartz.Impl;
using Tpf.Middlewares.Swagger;
//using Tpf.Common.CoreExtensions.DI;
using Autofac;
using Tpf.Autofac;
using Tpf.Middlewares.Log4Net;
using Tpf.Middlewares.Newtonsoft;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;
using Tpf.AutoMapper;
using Tpf.Authentication.Jwt;

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
            

            builder.Host.UseLog4Net();

            builder.Services.AddHealthChecks(); // HealthCheck

            builder.Services.AddControllers().AddNewtonsoftJsonMiddleware();

            builder.Services.AddSwaggerMiddleware(); // Swagger

            builder.Services.AddJwtBearerAuthentication();

            #region Add Middlewares
            //builder.Services.AddSingleton<AuthorizationMiddleware>();
            builder.Services.AddSingleton<ExceptionMiddleware>();

            //services.AddSingleton<IJobFactory, JobFactory>();
            //builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。

            builder.Services.AddAutoMapperMiddleware();
            #endregion

            #region IOC
            // 设置使用Autofac替换IOC容器
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(AutofacFactory.RegisterConfigure);

            #endregion

            #region 接口服务注册 Demo
            //.Net Core默认DI示例
            //services.AddTransient(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseService<>)); 
            #endregion

            #region Old (TODO: Drop)
            //builder.Services.AddTransient<ITencentCloudDBOperateService, TencentCloudDBOperateService>();

            #endregion

        }

    }

    
}
