using Tpf.Common.ConfigOptions;

namespace Tpf.BlobStoring.Minio
{
    /// <summary>
    /// MinioOptions
    /// </summary>
    public class MinioOptions : BaseOptions
    {
        public const string Name = "BlobStoring:Minio";

        public override string SectionName => "BlobStoring:Minio";

        /// <summary>
        /// 账号
        /// </summary>
        public string? AccessKey { get; set; }

        /// <summary>
        /// 密钥【已 AES 加密（16位密钥）】
        /// </summary>
        public string? SecretKey { get; set; }

        /// <summary>
        /// 服务端地址（ip:port）
        /// </summary>- 
        public string? EndPoint { get; set; }

        /// <summary>
        /// with HTTPS
        /// </summary>
        public bool? WithSSL { get; set; }

        
    }

}
