using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tpf.Grpc.Server;
using Tpf.Core.CoreExtensions.HostBuilderExtensions;

namespace Tpf.Ugly.Web
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tpf.Ugly", Version = "v1" });
            });

            #region gRpc Server
            services.AddGrpc();
            // 注册启用了代码优先的Grpc服务
            services.AddCodeFirstGrpc();
            // 注册启用反射的服务
            services.AddGrpcReflectionOfTPF();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            Console.WriteLine($"EnvironmentName: {env.EnvironmentName}");

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tpf.Ugly v1")); 
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // 异常Aop处理
            app.UseExceptionHandlerMidd();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                #region gRpc Server
                // TPF Grpc服务
                endpoints.MapGrpcServiceOfTPF();

                if (!env.IsProduction())
                {
                    // 添加Grpc反射服务终结点
                    endpoints.MapGrpcReflectionService();
                }
                #endregion
            });
        }
    }
}
