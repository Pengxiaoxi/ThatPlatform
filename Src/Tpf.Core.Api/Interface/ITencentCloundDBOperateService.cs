using Tpf.Core.Web.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TencentCloud.Mongodb.V20190725.Models;

namespace Tpf.Core.Web.Interface
{
    public interface ITencentCloudDBOperateService
    {
        Task<CreateBackupDBInstanceResponse> CreateBackupDBInstanceBySDK(string BackupRemark = null);

        Task<TencentCloud.Mongodb.V20190725.Models.BackupInfo> GetDescribeDBBackupsBySDK();

        Task<InstanceDetail> GetDescribeDBInstancesBySDK();

        Task<CreateBackupDownloadTaskResponse> CreateBackupDownloadTaskBySDK(TencentCloud.Mongodb.V20190725.Models.BackupInfo BackupInfo, ReplicaSetInfo[] ReplicaSetInfo);

        Task<DescribeBackupDownloadTaskResponse> GetDescribeBackupDownloadTaskSyncBySDK(TencentCloud.Mongodb.V20190725.Models.BackupInfo BackupInfo);

        Task DownloadCloudDBBackupFile();

        #region WebApi
        //Task<Dictionary<string, string>> GetRequestHeaders(string Action, object RequestPayload);

        //Task GetAndDownloadInstanceBackFile();

        //Task<DescribeDBBackupsResponseDto> GetDescribeDBBackups(DescribeDBBackupsRequestDto dto);

        //Task<DescribeBackupDownloadTaskResponseDto> GetDescribeBackupDownloadTask(DescribeBackupDownloadTaskRequestDto Dto); 
        #endregion

    }
}
