using Tpf.Core.Web.Dto;
using Tpf.Core.Web.Interface;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Mongodb.V20190725;
using TencentCloud.Mongodb.V20190725.Models;
using Tpf.Utils;

namespace Tpf.Core.Web.Service
{
    public class TencentCloudDBOperateService : ITencentCloudDBOperateService
    {
        #region Field
        protected readonly IConfiguration _configuration;
        protected readonly ILog _logger;

        protected string INSTANCEID = "";
        protected string SECRETID = "";
        protected string SECRETKEY = "";
        protected string ENDPOINT = "";
        protected string REGION = "";
        protected string ACTION = "";

        //protected string SERVICE = "";
        //protected string SERVICEURL = "";
        //protected string VERSION = "";

        // 文件下载存储路径
        protected static string FILEDIRECTORY = "D:\\FOne_FileBackup\\";

        protected MongodbClient _mongodbClient;
        #endregion

        #region Ctor
        public TencentCloudDBOperateService(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = LogManager.GetLogger(typeof(TencentCloudDBOperateService));

            //初始化配置参数
            this.Init();

            this._mongodbClient = this.GetMongodbClient();
        }
        #endregion

        #region Private
        private async Task Init()
        {
            var TencentColundDBAllConfig = _configuration.GetSection("TencentColundDBConfig").GetChildren();

            INSTANCEID = TencentColundDBAllConfig.FirstOrDefault(x => x.Key == "InstanceId").Value;
            if (string.IsNullOrEmpty(INSTANCEID))
            {
                _logger.Warn("实例ID不能为空");
            }
            SECRETID = TencentColundDBAllConfig.FirstOrDefault(x => x.Key == "SecretID").Value;
            SECRETKEY = TencentColundDBAllConfig.FirstOrDefault(x => x.Key == "SecretKey").Value;
            ENDPOINT = TencentColundDBAllConfig.FirstOrDefault(x => x.Key == "Endpoint").Value;
            REGION = TencentColundDBAllConfig.FirstOrDefault(x => x.Key == "Region").Value;

            //SERVICE = TencentColundDBAllConfig.FirstOrDefault(x => x.Key == "Service").Value;
            //SERVICEURL = TencentColundDBAllConfig.FirstOrDefault(x => x.Key == "ServiceUrl").Value;
            //VERSION = TencentColundDBAllConfig.FirstOrDefault(x => x.Key == "Version").Value;

            if (!string.IsNullOrEmpty(_configuration["FileSaveDirectory"]))
            {
                FILEDIRECTORY = _configuration["FileSaveDirectory"];
            }
        }
        #endregion


        #region 腾讯云SDK调用
        /// <summary>
        /// 创建MongodbClient
        /// </summary>
        /// <returns></returns>
        public MongodbClient GetMongodbClient()
        {
            Credential cred = new Credential
            {
                SecretId = SECRETID,
                SecretKey = SECRETKEY
            };
            ClientProfile clientProfile = new ClientProfile();
            HttpProfile httpProfile = new HttpProfile
            {
                Endpoint = ENDPOINT
            };
            clientProfile.HttpProfile = httpProfile;
            clientProfile.SignMethod = ClientProfile.SIGN_TC3SHA256;
            MongodbClient client = new MongodbClient(cred, REGION, clientProfile);
            return client;
        }

        /// <summary>
        /// 备份实例 By SDK
        /// </summary>
        /// <param name="BackupRemark">备份备注</param>
        /// <returns></returns>
        public async Task<CreateBackupDBInstanceResponse> CreateBackupDBInstanceBySDK(string BackupRemark = null)
        {
            // 备份方式：0-逻辑备份，1-物理备份
            var backupMethod = _configuration["BackupMethod"];
            CreateBackupDBInstanceRequest createBackupReq = new CreateBackupDBInstanceRequest()
            {
                InstanceId = INSTANCEID,
                BackupMethod = string.IsNullOrEmpty(backupMethod) ? 0 : Convert.ToInt32(backupMethod),
                BackupRemark = BackupRemark
            };
            _logger.Info($"CreateBackupDBInstanceRequest：{JsonConvert.SerializeObject(createBackupReq)}");

            CreateBackupDBInstanceResponse createBackupRep = await _mongodbClient.CreateBackupDBInstance(createBackupReq);
            _logger.Info($"CreateBackupDBInstanceResponse：{JsonConvert.SerializeObject(createBackupRep)}");

            return createBackupRep;
        }

        /// <summary>
        /// 查询腾讯云MongoDB数据库实例备份列表 By SDK
        /// </summary>
        /// <returns></returns>
        public async Task<TencentCloud.Mongodb.V20190725.Models.BackupInfo> GetDescribeDBBackupsBySDK()
        {
            DescribeDBBackupsRequest req = new DescribeDBBackupsRequest
            {
                InstanceId = INSTANCEID
            };
            DescribeDBBackupsResponse resp = await _mongodbClient.DescribeDBBackups(req);
            _logger.Info($"DescribeDBBackupsResponse：{JsonConvert.SerializeObject(resp)}");

            var newestBackInfo = resp?.BackupList.Where(x => x.Status == 2 && !string.IsNullOrEmpty(x.EndTime))
                .OrderByDescending(x => x.StartTime)
                .FirstOrDefault();
            return newestBackInfo;
        }

        /// <summary>
        /// 查询实例详细信息
        /// </summary>
        /// <returns></returns>
        public async Task<InstanceDetail> GetDescribeDBInstancesBySDK()
        {
            var req = new DescribeDBInstancesRequest()
            {
                InstanceIds = new string[] { INSTANCEID }
            };
            DescribeDBInstancesResponse instancesRep = await _mongodbClient.DescribeDBInstances(req);
            _logger.Info($"DescribeDBInstancesResponse：{JsonConvert.SerializeObject(instancesRep)}");
            return instancesRep?.InstanceDetails.FirstOrDefault();
        }

        /// <summary>
        /// 创建备份下载任务
        /// </summary>
        /// <param name="BackupInfo"></param>
        /// <param name="ReplicaSetInfo"></param>
        /// <returns></returns>
        public async Task<CreateBackupDownloadTaskResponse> CreateBackupDownloadTaskBySDK(TencentCloud.Mongodb.V20190725.Models.BackupInfo BackupInfo, ReplicaSetInfo[] ReplicaSetInfo)
        {
            CreateBackupDownloadTaskRequest req = new CreateBackupDownloadTaskRequest()
            {
                InstanceId = INSTANCEID,
                BackupName = BackupInfo?.BackupName,
                BackupSets = ReplicaSetInfo
            };
            CreateBackupDownloadTaskResponse createDownTaskRep = await _mongodbClient.CreateBackupDownloadTask(req);
            _logger.Info($"CreateBackupDownloadTaskResponse：{JsonConvert.SerializeObject(createDownTaskRep)}");
            return createDownTaskRep;
        }

        /// <summary>
        /// 查询腾讯云MongoDB数据库实例备份下载任务信息 By SDK
        /// </summary>
        /// <param name="BackupList"></param>
        /// <returns></returns>
        public async Task<DescribeBackupDownloadTaskResponse> GetDescribeBackupDownloadTaskSyncBySDK(TencentCloud.Mongodb.V20190725.Models.BackupInfo BackupInfo)
        {
            
            DescribeBackupDownloadTaskRequest req = new DescribeBackupDownloadTaskRequest
            {
                InstanceId = INSTANCEID,
                BackupName = BackupInfo?.BackupName
            };
            DescribeBackupDownloadTaskResponse resp = await _mongodbClient.DescribeBackupDownloadTask(req);
            _logger.Info($"DescribeBackupDownloadTaskResponse：{JsonConvert.SerializeObject(resp)}");
            return resp;
        }

        /// <summary>
        /// 查询备份文件信息并下载备份文件
        /// </summary>
        /// <returns></returns>
        public async Task DownloadCloudDBBackupFile()
        {
            // 1、备份文件列表信息
            var buckUpInfoResult = await this.GetDescribeDBBackupsBySDK();
            if(buckUpInfoResult is null)
            {
                this.LogInfo($"未查询到当前实例备份文件列表信息，实例ID:{INSTANCEID}");

                // 获取配置：是否创建备份，备份方式
                var isCreateBackupDBInstance = _configuration["isCreateBackupDBInstance"];
                if (!string.IsNullOrEmpty(isCreateBackupDBInstance) && Convert.ToBoolean(isCreateBackupDBInstance))
                {
                    await this.CreateBackupDBInstanceBySDK(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                return;
            }

            // 2、获取实例详细信息
            var instanceDetail = await this.GetDescribeDBInstancesBySDK();
            if(instanceDetail is null)
            {
                this.LogInfo($"未查询到当前实例详细信息，实例ID:{INSTANCEID}");
                return;
            }

            // 备份文件存储路径
            var actualFileDirectory = Path.Combine(FILEDIRECTORY, INSTANCEID, DateTime.Now.ToString("yyyy-MM-dd"));

            #region 3、创建备份文件下载任务（按分片下载）
            var allReplicaSetIds = instanceDetail?.ReplicaSets.Select(x => x.ReplicaSetId).ToArray();
            var replicaSetInfoList = new List<ReplicaSetInfo>();
            for (int i = 0; i < allReplicaSetIds.Length; i++)
            {
                // 如果当前分片的文件已存在则不再进行下载操作，不存在则继续创建下载任务并进行下载
                var backFileName = $"{buckUpInfoResult?.BackupName.Replace(':', '-')}_{allReplicaSetIds[i]}.tar";
                if (File.Exists(Path.Combine(actualFileDirectory, backFileName)))
                {
                    continue;
                }
                replicaSetInfoList.Add(new ReplicaSetInfo() { ReplicaSetId = allReplicaSetIds[i] });
            }
            if(replicaSetInfoList.Count == 0)
            {
                this.LogInfo($"当前实例最新备份文件已全部下载，无需继续下载，实例ID:{INSTANCEID}");
                return;
            }

            var createDownTaskResult = await this.CreateBackupDownloadTaskBySDK(buckUpInfoResult, replicaSetInfoList.ToArray());
            var downTasks = createDownTaskResult?.Tasks;
            if (downTasks == null || downTasks.Length == 0)
            {
                this.LogInfo($"当前实例创建下载任务后未返回下载信息，实例ID:{INSTANCEID}");
                return;
            }
            // 如果response存在创建失败的下载任务，则重新创建失败分区的下载任务 (Status == 3 创建下载任务失败)
            var downFaildTasks = downTasks.Where(x => x.Status == 3).ToList();
            if (downFaildTasks.Count > 0)
            {
                var faildReplicaIds = downFaildTasks.Select(x => x.ReplicaSetId).ToArray();
                var faildReplicaIdInfo = new ReplicaSetInfo[faildReplicaIds.Length];
                for (int i = 0; i < faildReplicaIds.Length; i++)
                {
                    faildReplicaIdInfo[i] = new ReplicaSetInfo() { ReplicaSetId = faildReplicaIds[i] };
                }
                var reTryDownTasks = await this.CreateBackupDownloadTaskBySDK(buckUpInfoResult, faildReplicaIdInfo);
            }
            #endregion

            // 创建备份下载任务后，腾讯云需要一定的时间处理下载，因此此处需要等待一定时间后才能下载到备份文件【或下一次执行此任务时去进行下载】
            Thread.Sleep(30 * 1000);

            #region 4、下载备份文件（Tasks按分片区分）
            var taskResult = await this.GetDescribeBackupDownloadTaskSyncBySDK(buckUpInfoResult);
            var allSuccessTasks = taskResult?.Tasks.Where(x => x.Status == 2);
            foreach (var task in allSuccessTasks)
            {
                var fileName = $"{buckUpInfoResult?.BackupName.Replace(':', '-')}_{task?.ReplicaSetId}.tar";
                FileHelper.DownloadWebServerFile(task?.Url, actualFileDirectory, fileName);
                this.LogInfo($"备份文件 {fileName} 下载成功");
            }
            #endregion
        }

        /// <summary>
        /// 控制台打印+记录日志
        /// </summary>
        /// <param name="Message"></param>
        public void LogInfo(string Message = null)
        {
            if(!string.IsNullOrEmpty(Message))
            {
                Message = $"{DateTime.Now}：{Message}";
                Console.WriteLine(Message);
                _logger.Info(Message);
            }
        }
        #endregion


        #region WebApi调用 未使用

        #region WebApi 签名生成及通用请求方法
        /// <summary>
        /// GetRequestHeaders
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="RequestPayload">请求参数对象</param>
        /// <returns></returns>
        //public async Task<Dictionary<string, string>> GetRequestHeaders(string Action, object RequestPayload)
        //{
        //    var requesHeaderDto = TencentCloundSignatrue.GenerateRequestDto(SECRETID, SECRETKEY, SERVICE, ENDPOINT, REGION, VERSION, Action, RequestPayload);
        //    var curRequestHeaders = TencentCloundSignatrue.GenerateHeaders(requesHeaderDto);
        //    return curRequestHeaders;
        //}

        //public async Task<T> TCPost<T>(string Action, object RequestPayload) where T : BaseTCCloudDBResponseDTO
        //{
        //    try
        //    {
        //        _logger.Info($"Action：{Action}");
        //        Console.WriteLine($"Action：{Action}");

        //        var headers = await this.GetRequestHeaders(Action, RequestPayload);

        //        _logger.Info($"RequestHeaders：{JsonConvert.SerializeObject(headers)}");
        //        _logger.Info($"RequestPayload：{JsonConvert.SerializeObject(RequestPayload)}");
        //        Console.WriteLine($"RequestHeaders：{JsonConvert.SerializeObject(headers)}");
        //        Console.WriteLine($"RequestPayload：{JsonConvert.SerializeObject(RequestPayload)}");

        //        var response = WebApiHelper.HttpPost<BaseResponse<T>>(SERVICEURL, RequestPayload, headers);
        //        _logger.Info($"Response：{JsonConvert.SerializeObject(response)}");
        //        Console.WriteLine($"Response：{JsonConvert.SerializeObject(response)}");

        //        //接口返回错误
        //        var error = response?.Response?.Error;
        //        if (error != null && !string.IsNullOrEmpty(error?.Message))
        //        {
        //            throw new Exception($"Code：{error?.Code}，Message：{error?.Message}");
        //        }

        //        return response?.Response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Warn(ex);
        //        Console.WriteLine(ex);
        //        return null;
        //    }
        //}
        #endregion


        /// <summary>
        /// 查询实例备份列表并进行下载
        /// </summary>
        /// <returns></returns>
        //public async Task GetAndDownloadInstanceBackFile()
        //{
        //    try
        //    {
        //        var requestDto = new DescribeDBBackupsRequestDto
        //        {
        //            //Version = VERSION,
        //            //Region = REGION,
        //            InstanceId = INSTANCEID
        //        };

        //        #region 1、获取实例备份列表
        //        var result = await this.GetDescribeDBBackups(requestDto);

        //        if (result == null)
        //        {
        //            return;
        //        }
        //        if (result?.BackupList == null || result?.BackupList.Count == 0)
        //        {
        //            _logger.Info($"当前实例 {requestDto?.InstanceId} 备份列表为空");
        //            return;
        //        }

        //        // 获取已备份完成且备份开始与结束时间都不为空的，最新的备份开始时间的备份信息
        //        var curNeedDownBackupInfo = result?.BackupList.Where(x => x.Status == 2
        //            && !string.IsNullOrEmpty(x.StartTime)
        //            && !string.IsNullOrEmpty(x.EndTime))
        //            .OrderByDescending(x => x.StartTime)
        //            .FirstOrDefault();
        //        #endregion

        //        #region 2、通过符合条件实例信息查询备份下载任务信息
        //        //默认不查询下载中和下载完成的任务，只查询未下载和下载失败
        //        var statusArr = new int?[] { 0, 3, 4 };
        //        var taskRequestDto = new DescribeBackupDownloadTaskRequestDto
        //        {
        //            Version = VERSION,
        //            Region = REGION,
        //            InstanceId = curNeedDownBackupInfo?.InstanceId,
        //            BackupName = curNeedDownBackupInfo?.BackupName,
        //            Status = statusArr
        //        };
        //        var taskResult = await this.GetDescribeBackupDownloadTask(taskRequestDto);
        //        if (taskResult == null || taskResult?.Tasks == null || taskResult?.Tasks.Count == 0)
        //        {
        //            _logger.Info($"当前实例 {taskRequestDto?.InstanceId} 备份下载任务列表为空");
        //            return;
        //        }

        //        var allTasks = taskResult?.Tasks.Where(x => statusArr.Contains(x.Status)).ToList();
        //        foreach (var task in allTasks)
        //        {
        //            if (string.IsNullOrEmpty(task?.Url))
        //            {
        //                continue;
        //            }

        //            //保存文件名：备份文件名-分片名称
        //            var fileName = $"{task?.BackupName}_{task?.ReplicaSetId}.dbtar";
        //            var actualFileDirectory = Path.Combine(FILEDIRECTORY, INSTANCEID);
        //            FileHelper.DownloadWebServerFile(task?.Url, actualFileDirectory, fileName);

        //            var tipMsg = $"备份文件{fileName}下载成功";
        //            _logger.Info(tipMsg);
        //            Console.WriteLine(tipMsg);
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Warn(ex);
        //        Console.WriteLine(ex);
        //    }
        //}

        /// <summary>
        /// 查询腾讯云MongoDB数据库实例备份列表
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        //public async Task<DescribeDBBackupsResponseDto> GetDescribeDBBackups(DescribeDBBackupsRequestDto Dto)
        //{
        //    var action = "DescribeDBBackups";
        //    //Dto.Action = action;
        //    var result = await this.TCPost<DescribeDBBackupsResponseDto>(action, Dto);
        //    return result;
        //}

        /// <summary>
        /// 查询腾讯云MongoDB数据库实例备份下载任务信息
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        //public async Task<DescribeBackupDownloadTaskResponseDto> GetDescribeBackupDownloadTask(DescribeBackupDownloadTaskRequestDto Dto)
        //{
        //    var action = "DescribeBackupDownloadTask";
        //    Dto.Action = action;
        //    var result = await this.TCPost<DescribeBackupDownloadTaskResponseDto>(action, Dto);
        //    return result;
        //}
        #endregion
    }
}
