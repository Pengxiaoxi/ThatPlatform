using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tpf.Core.Api.Dto
{
    /// <summary>
    /// 查询实例备份列表 Reqeust&Reponse Dto
    /// </summary>
    public class DescribeDBBackupsRequestDto : BaseTCCloudDBRequestDto
    {
        /// <summary>
        ///【非必选】备份方式，当前支持：0-逻辑备份，1-物理备份，2-所有备份。默认为逻辑备份
        /// </summary>
        public int? BackupMethod { get; set; }

        /// <summary>
        ///【非必选】分页大小，最大值为100，不设置默认查询所有
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// 非必选】分页偏移量，最小值为0，默认值为0
        /// </summary>
        public int? Offset { get; set; }

    }

    public class DescribeDBBackupsResponseDto : BaseTCCloudDBResponseDTO
    {
        /// <summary>
        /// 备份列表
        /// </summary>
        public List<BackupInfo> BackupList { get; set; }

        /// <summary>
        /// 备份总数
        /// </summary>
        public int TotalCount { get; set; }

    }

    public class BackupInfo
    {
        /// <summary>
        /// 实例ID
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// 备份方式，0-自动备份，1-手动备份
        /// </summary>
        public int BackupType { get; set; }

        /// <summary>
        /// 备份名称
        /// </summary>
        public string BackupName { get; set; }

        /// <summary>
        /// 备份备注
        /// </summary>
        public string BackupDesc { get; set; }

        /// <summary>
        /// 备份文件大小，单位KB 注意：此字段可能返回 null，表示取不到有效值。
        /// </summary>
        public int? BackupSize { get; set; }

        /// <summary>
        /// 备份开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 备份结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 备份状态，1-备份中，2-备份成功
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备份方法，0-逻辑备份，1-物理备份
        /// </summary>
        public int BackupMethod { get; set; }
    }

}
