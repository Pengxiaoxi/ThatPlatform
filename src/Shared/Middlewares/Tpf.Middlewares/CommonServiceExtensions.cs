﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tpf.Authentication.Jwt;
using Tpf.Autofac;
using Tpf.AutoMapper;
using Tpf.Caching.CSRedisCore;
using Tpf.Middlewares.Log4Net;
using Tpf.Middlewares.Newtonsoft;
using Tpf.Middlewares.Options;
using Tpf.Middlewares.Swagger;
using Tpf.SqlSugar;

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

            builder.Services.AddHttpContextAccessor(); // IHttpContextAccessor

            

            #region Add Middlewares
            //builder.Services.AddSingleton<AuthorizationMiddleware>();
            builder.Services.AddSingleton<ExceptionMiddleware>();

            //services.AddSingleton<IJobFactory, JobFactory>();
            //builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。

            builder.Services.AddAutoMapperMiddleware();

            // ORM: SqlSugar
            builder.Services.AddSqlSugar();

            

            #endregion

            #region IOC
            // 设置使用Autofac替换IOC容器
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(AutofacFactory.RegisterConfigure);

            #endregion

            #region AddTpfOptions
            //builder.AddTpfOptions();

            #endregion

            builder.AddTpfCSRedisCore();


            #region 接口服务注册 Demo
            //.Net Core默认DI示例
            //services.AddTransient(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseService<>)); 
            #endregion

            #region Onsolute (TODO: Drop)
            //builder.Services.AddTransient<ITencentCloudDBOperateService, TencentCloudDBOperateService>();

            #endregion

        }

    }

    
}
