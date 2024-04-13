using Microsoft.Extensions.DependencyInjection;
using Minio;
using Tpf.Common.Config;
using Tpf.Utils;

namespace Tpf.BlobStoring.Minio
{
    public static class MinIOServiceExtensions
    {
        /// <summary>
        /// Minio Client
        /// </summary>
        /// <param name="services"></param>
        public static void AddMinioClient(this IServiceCollection services)
        {
            //var accessKey = AppSettings.App("MinIO", "AccessKey");
            //var secretKey = AppSettings.App("MinIO", "SecretKey");
            //var endPoint = AppSettings.App("MinIO", "EndPoint");

            var accessKey = ConfigHelper.Get(AppConfig.Minio_AccessKey);
            var secretKey = ConfigHelper.Get(AppConfig.Minio_SecretKey);
            var endPoint = ConfigHelper.Get(AppConfig.Minio_EndPoint);
            var withSSL = Convert.ToBoolean(ConfigHelper.Get(AppConfig.Minio_WithSSL));
            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(endPoint))
            {
                throw new Exception("GetMinioClient faild, MinIO config is null.");
            }

            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(endPoint))
            {
                return;
            }

            services.AddMinio(configureClient =>
            {
                configureClient
                    .WithCredentials(accessKey, secretKey)
                    .WithEndpoint(endPoint)
                    //.WithRegion()
                    .WithTimeout(60 * 1000) // 60s
                    .WithSSL(withSSL)
                    ;
            });
        }
    }
}
