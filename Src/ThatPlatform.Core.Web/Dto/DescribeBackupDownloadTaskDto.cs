using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThatPlatform.Core.Web.Dto
{
    /// <summary>
    /// 查询备份下载任务信息 Reqeust&Reponse Dto
    /// https://cloud.tencent.com/document/product/240/54174
    /// </summary>
    public class DescribeBackupDownloadTaskRequestDto : BaseTCCloudDBRequestDto
    {
        /// <summary>
        /// 【非必选】备份文件名，用来过滤指定文件的下载任务
        /// </summary>
        public string BackupName { get; set; }

        /// <summary>
        /// 【非必选】指定要查询任务的时间范围，StartTime指定开始时间，不填默认不限制开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 【非必选】指定要查询任务的时间范围，EndTime指定结束时间，不填默认不限制结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 【非必选】此次查询返回的条数，取值范围为1-100，默认为20
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 【非必选】指定此次查询返回的页数，默认为0
        /// </summary>
        public string Offset { get; set; }

        /// <summary>
        /// 【非必选】排序字段，取值为createTime，finishTime两种，默认为createTime
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 【非必选】排序方式，取值为asc，desc两种，默认desc
        /// </summary>
        public string OrderByType { get; set; }

        /// <summary>
        /// 【非必选】根据任务状态过滤。0-等待执行，1-正在下载，2-下载完成，3-下载失败，4-等待重试。不填默认返回所有类型
        /// </summary>
        public int?[] Status { get; set; }

    }

    public class DescribeBackupDownloadTaskResponseDto : BaseTCCloudDBResponseDTO
    {
        /// <summary>
        /// 满足查询条件的所有条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 下载任务列表
        /// </summary>
        public List<BackupDownloadTask> Tasks { get; set; }

    }

    /// <summary>
    /// 备份下载任务
    /// </summary>
    public class BackupDownloadTask
    {
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 备份文件名
        /// </summary>
        public string BackupName { get; set; }

        /// <summary>
        /// 分片名称
        /// </summary>
        public string ReplicaSetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MyProperty { get; set; }

        /// <summary>
        /// 备份数据大小，单位为字节
        /// </summary>
        public int BackupSize { get; set; }

        /// <summary>
        /// 任务状态。0-等待执行，1-正在下载，2-下载完成，3-下载失败，4-等待重试
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 任务进度百分比
        /// </summary>
        public int Percent { get; set; }

        /// <summary>
        /// 耗时，单位为秒
        /// </summary>
        public int TimeSpend { get; set; }

        /// <summary>
        /// 备份数据下载链接
        /// </summary>
        public string Url { get; set; }

    }
}
