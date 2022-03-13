using ThatPlatform.Core.Web.Interface;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThatPlatform.Core.Web
{
    public class DownloadTaskService : BackgroundService
    {
        #region Field
        protected readonly ILog _logger;
        protected readonly IConfiguration _configuration;
        protected readonly ITencentCloudDBOperateService _operateService;

        #endregion

        #region Ctor
        public DownloadTaskService(IConfiguration configuration,
            ITencentCloudDBOperateService operateService)
        {
            _logger = LogManager.GetLogger(typeof(DownloadTaskService));
            _configuration = configuration;
            _operateService = operateService;
        } 
        #endregion

        #region override
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            #region 通过配置处理是否定时启动定时任务
            var isRunDownloadTask = _configuration["IsRunDownloadTask"];
            if (string.IsNullOrEmpty(isRunDownloadTask) || Convert.ToBoolean(isRunDownloadTask) == false)
            {
                return;
            } 
            #endregion

            // 是否固定时间点执行
            var isTimedExecution = Convert.ToBoolean(_configuration["IsTimedExecution"]);
            // 定时任务执行间隔（单位：毫秒）
            var timeIntervalStr = _configuration["BackgroundTaskTimeInterval"];
            
            // 轮询时间间隔（默认1s）,非固定时间点执行则取配置时间间隔
            int timeInterval = 1 * 1000;
            if (!isTimedExecution)
            {
                if (string.IsNullOrEmpty(timeIntervalStr))
                {
                    timeIntervalStr = "1*60*1000";
                }
                var intervalArr = timeIntervalStr.Split("*");
                timeInterval = timeInterval / 1000;
                for (int i = 0; i < intervalArr.Length; i++)
                {
                    timeInterval = timeInterval * Convert.ToInt32(intervalArr[i]);
                }
            }

            // 固定执行时间点
            var executeTime = _configuration["BackgroundTaskExecTime"];
            var timeArr = executeTime.Split(":");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // 定时
                    if (isTimedExecution && timeArr.Length > 1)
                    {
                        var time = DateTime.Now;
                        if (time.Second.Equals(0) && time.Hour.Equals(Convert.ToInt32(timeArr[0])) && time.Minute.Equals(Convert.ToInt32(timeArr[1])))
                        {
                            await Callback();
                        }
                    }
                    // 轮询
                    else
                    {
                        await Callback();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    _logger.Warn(ex);
                }
                // 间隔的时间
                await Task.Delay(timeInterval);
            }
        } 
        #endregion

        /// <summary>
        /// 定时执行Action
        /// </summary>
        /// <returns></returns>
        private async Task Callback()
        {
            // WebApi
            //await _operateService.GetAndDownloadInstanceBackFile();

            // SDK
            await _operateService.DownloadCloudDBBackupFile();
        }
    }
}
