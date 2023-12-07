using Minio;

namespace Tpf.FileServer.Minio
{
    /// <summary>
    /// Minio FileServer
    /// Minio Docs: https://min.io/docs/minio/linux/developers/dotnet/quickstart.html#minio-client-sdk-for-net
    /// </summary>
    public interface IMinioService
    {
        public IMinioClient Client { get; }

        /// <summary>
        /// Check the bucket exists
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        Task<bool> ExistsBucket(string bucketName);

        /// <summary>
        /// Create a bucket
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        Task<bool> CraeteBucket(string bucketName);

        /// <summary>
        /// Upload multi files
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> UploadMultiFile(List<UploadFileArg> args);

        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        Task<MemoryStream> DownloadFile(DownloadFileArg arg);

    }
}
