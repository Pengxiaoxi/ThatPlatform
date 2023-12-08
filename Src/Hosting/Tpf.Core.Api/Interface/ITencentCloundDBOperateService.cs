using System.Threading.Tasks;
using TencentCloud.Mongodb.V20190725.Models;

namespace Tpf.Tool.DbOperate.Api.Interface
{
    public interface ITencentCloudDBOperateService
    {
        Task<CreateBackupDBInstanceResponse> CreateBackupDBInstanceBySDK(string BackupRemark = null);

        Task<BackupInfo> GetDescribeDBBackupsBySDK();

        Task<InstanceDetail> GetDescribeDBInstancesBySDK();

        Task<CreateBackupDownloadTaskResponse> CreateBackupDownloadTaskBySDK(BackupInfo BackupInfo, ReplicaSetInfo[] ReplicaSetInfo);

        Task<DescribeBackupDownloadTaskResponse> GetDescribeBackupDownloadTaskSyncBySDK(BackupInfo BackupInfo);

        Task DownloadCloudDBBackupFile();

        #region WebApi
        //Task<Dictionary<string, string>> GetRequestHeaders(string Action, object RequestPayload);

        //Task GetAndDownloadInstanceBackFile();

        //Task<DescribeDBBackupsResponseDto> GetDescribeDBBackups(DescribeDBBackupsRequestDto dto);

        //Task<DescribeBackupDownloadTaskResponseDto> GetDescribeBackupDownloadTask(DescribeBackupDownloadTaskRequestDto Dto); 
        #endregion

    }
}
