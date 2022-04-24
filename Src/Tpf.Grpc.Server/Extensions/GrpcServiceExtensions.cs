using Microsoft.Extensions.DependencyInjection;


namespace Tpf.Grpc.Server
{
    /// <summary>
    /// GrpcServiceExtension
    /// </summary>
    public static class GrpcServiceExtensions
    {
        /// <summary>
        /// AddGrpcReflectionOfTPF 注册启用反射的服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddGrpcReflectionOfTPF(this IServiceCollection services)
        {
            services.AddGrpcReflection();
        }
    }
}
