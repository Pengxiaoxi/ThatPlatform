using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Minio;

namespace Tpf.BlobStoring.Minio
{
    public static class MinioServiceExtensions
    {
        /// <summary>
        /// Minio Client
        /// </summary>
        /// <param name="services"></param>
        public static void AddMinioClient(this IHostApplicationBuilder builder)
        {
            var minioOptions = builder.Configuration.GetSection(new MinioOptions().SectionName)?.Get<MinioOptions>();
            var withSSL = minioOptions?.WithSSL ?? false;

            if (string.IsNullOrEmpty(minioOptions?.AccessKey) || string.IsNullOrEmpty(minioOptions ?.SecretKey) || string.IsNullOrEmpty(minioOptions?.EndPoint))
            {
                throw new Exception("GetMinioClient faild, MinIO config is null.");
            }

            if (string.IsNullOrEmpty(minioOptions.AccessKey) || string.IsNullOrEmpty(minioOptions.SecretKey) || string.IsNullOrEmpty(minioOptions.EndPoint))
            {
                return;
            }

            builder.Services.AddMinio(configureClient =>
            {
                configureClient
                    .WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey)
                    .WithEndpoint(minioOptions.EndPoint)
                    //.WithRegion()
                    .WithTimeout(60 * 1000) // 60s
                    .WithSSL(withSSL)
                    ;
            });
        }
    }
}
