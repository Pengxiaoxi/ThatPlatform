using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Tpf.Middlewares.Swagger;

namespace Tpf.Middlewares
{
    public static class CommonAppMiddlewares
    {
        public static void UseCommonAppMiddlewares(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }


            //app.UseStaticFiles();

            //app.UseRouting();

            app.UseExceptionMiddleware(); // ExceptionMiddleware

            //app.UseAuthorizationMiddleware(); // Authorization Middleware

            app.UseAuthentication(); // 鉴权校验

            //app.UseAuthorization(); // 授权校验

            app.UseHealthChecks("/health"); // HealthCheck

            app.UseKnife4UI(); // Swagger + Knife4UI

            

            app.MapControllers();


            
        }

    }
}
