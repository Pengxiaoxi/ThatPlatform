using Microsoft.Extensions.DependencyInjection;
using Tpf.Dapper.Repository;

namespace Tpf.Dapper
{
    public static class DapperServcieExtensions
    {
        /// <summary>
        /// AddGrpcReflectionOfTPF 注册启用反射的服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddTpfDapper(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDapperRepository<>), typeof(DapperRepository<>));
        }
    }
}
