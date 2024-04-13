using System.Diagnostics.CodeAnalysis;

namespace Tpf.BlobStoring.Minio
{
    public class UploadFileArg
    {
        /// <summary>
        /// 文件桶名称
        /// </summary>
        [NotNull]
        public string? BucketName { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [NotNull]
        public string? FileName { get; set; }

        /// <summary>
        /// FileStream
        /// </summary>
        [NotNull]
        public Stream? FileStream { get; set; }
    }

    public class DownloadFileArg
    {
        /// <summary>
        /// 文件桶名称
        /// </summary>
        [NotNull]
        public string? BucketName { get; set; }

        /// <summary>
        /// 对象名称（完整路径，例如：2023-12-12/cb2d7b9b-cfe2-4011-8026-b853c1263517.txt）
        /// </summary>
        [NotNull]
        public string? ObjectName { get; set; }

        /// <summary>
        /// 指定文件名称
        /// </summary>
        [NotNull]
        public string? FileName { get; set; }
    }
}
