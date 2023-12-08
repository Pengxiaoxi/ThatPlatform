using Minio;
using Minio.DataModel.Args;
using Tpf.Common.Config;
using Tpf.Utils;

namespace Tpf.BlobStoring.Minio
{
    public class MinioService : IMinioService
    {
        private static IMinioClient _client;

        public IMinioClient Client
        {
            get => GetMinioClient();
        }

        public MinioService()
        {

        }

        /// <summary>
        /// Bucket是否已存在
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task<bool> ExistsBucket(string bucketName)
        {
            return await Client.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        }

        /// <summary>
        /// Bucket创建
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task<bool> CraeteBucket(string bucketName)
        {
            await Client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            return true;
        }

        /// <summary>
        /// 批量上传文件
        /// Minio, Version=6.0.1.0 暂无批量上传api接口，foreach实现
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> UploadMultiFile(List<UploadFileArg> args)
        {
            var result = new Dictionary<string, string>();

            try
            {
                foreach (var arg in args)
                {
                    // Bucket不存在则创建
                    if (!await ExistsBucket(arg.BucketName))
                    {
                        await CraeteBucket(arg.BucketName);
                    }

                    var objectName = GetObjectName(arg.FileName);
                    arg.FileStream.Seek(0, SeekOrigin.Begin);

                    var putObjArgs = new PutObjectArgs()
                        .WithBucket(arg.BucketName)
                        .WithObject(objectName)
                        .WithStreamData(arg.FileStream)
                        .WithObjectSize(arg.FileStream.Length)
                        ;

                    await GetMinioClient().PutObjectAsync(putObjArgs); // MinIO DotNet SDK 暂无批量上传接口

                    var objectShareUrl = await GetMinioClient().PresignedGetObjectAsync(new PresignedGetObjectArgs()
                        .WithBucket(arg.BucketName)
                        .WithObject(objectName)
                        .WithExpiry(604800)
                        );

                    result.Add(arg.FileName, objectShareUrl);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"File upload faild by Minio，{ex.Message}");
            }
        }

        public async Task<MemoryStream> DownloadFile(DownloadFileArg arg)
        {
            try
            {
                var fileStrem = new MemoryStream();

                var getObjArgs = new GetObjectArgs()
                    .WithBucket(arg.BucketName)
                    .WithObject(arg.ObjectName)
                    .WithCallbackStream((stream) =>
                    {
                        stream.CopyTo(fileStrem);
                    });
                await GetMinioClient().GetObjectAsync(getObjArgs);

                return fileStrem;
            }
            catch (Exception ex)
            {
                throw new Exception($"File download faild by Minio，{ex.Message}");
            }
        }

        #region Private Method
        /// <summary>
        /// GetMinioClient
        /// </summary>
        /// <returns></returns>
        private static IMinioClient GetMinioClient()
        {
            try
            {
                if (_client != null)
                {
                    return _client;
                }

                var accessKey = ConfigHelper.Get(AppConfig.Minio_AccessKey);
                var secretKey = ConfigHelper.Get(AppConfig.Minio_SecretKey); ;
                var endPoint = ConfigHelper.Get(AppConfig.Minio_EndPoint); ;
                var withSSL = ConfigHelper.Get(AppConfig.Minio_WithSSL); ;
                if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(endPoint))
                {
                    throw new Exception("GetMinioClient faild, MinIO config is null.");
                }

                // TODO: secretKey 解密

                var client = new MinioClient()
                    .WithEndpoint(endPoint)
                    .WithCredentials(accessKey, secretKey);

                if (!string.IsNullOrEmpty(withSSL) && Convert.ToBoolean(withSSL))
                {
                    client.WithSSL();
                }

                _client = client.Build();
                return _client;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 对象名称（保存目录+名称 /日期/NewFileName）
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetObjectName(string fileName)
        {
            var newFileName = Guid.NewGuid().ToString();
            if (fileName.Contains('.'))
            {
                var fileExtension = fileName?.Split('.')[1];
                newFileName = $"{newFileName}.{fileExtension}";
            }

            return $"{DateTime.Now.Date}/{newFileName}";
        }



        #endregion
    }
}
