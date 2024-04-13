using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Common.Config
{
    public partial class AppConfig
    {
        public const string Minio_AccessKey = "FileServer:Minio:AccessKey";
        public const string Minio_SecretKey = "FileServer:Minio:SecretKey";
        public const string Minio_EndPoint = "FileServer:Minio:EndPoint";
        public const string Minio_WithSSL = "FileServer:Minio:WithSSL";

    }
}
