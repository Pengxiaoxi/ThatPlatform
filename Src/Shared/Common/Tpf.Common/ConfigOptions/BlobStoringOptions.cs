using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tpf.Common.ConfigOptions
{
    public class BlobStoringOptions : BaseOptions
    {
        public const string Name = "BlobStoring";

        public override string GetOptionsName()
        {
            return "BlobStoring";
        }

        public MinioOptions? Minio { get; set; }

        
    }

    /// <summary>
    /// Minio
    /// </summary>
    public class MinioOptions : IBaseOptions
    {
        //public override string GetOptionsName()
        //{
        //    return "BlobStoring:Minio";
        //}

        //public string CurrentOptionsName { get => "BlobStoring:Minio"; }

        //public string CurrentOptionsName()
        //{
        //    return "BlobStoring:Minio";
        //}

        public const string Name = "BlobStoring:Minio";

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
