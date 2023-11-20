using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.Server;
using System;
using Tpf.Grpc.Server.Extensions;
using Tpf.Middlewares;

namespace Tpf.Ugly.Api
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
            // ָ��Swaggerʹ��Newtonsoft.Json���л�������Swagger�ӿ��ĵ��ڽӿڲ������������JsonProperty������
            services.AddSwaggerGenNewtonsoftSupport();

            #region gRpc Server
            services.AddGrpc();
            // ע�������˴������ȵ�Grpc����
            services.AddCodeFirstGrpc();
            // ע�����÷���ķ���
            //services.AddGrpcReflectionOfTPF();
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

            app.UseExceptionMiddleware(); // �쳣Aop����

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                #region gRpc Server
                // TPF Grpc����
                endpoints.MapGrpcService();

                if (!env.IsProduction())
                {
                    // ���Grpc��������ս��
                    endpoints.MapGrpcReflectionService();
                }
                #endregion
            });
        }
    }
}
