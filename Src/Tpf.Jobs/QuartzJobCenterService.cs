using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.Core.CommonAttributes;
using Tpf.Utils.DevExtensions.ServiceResult;

namespace Tpf.Jobs.QuartzNet
{
    // TODO: 目前是单调度器，是否可调整为多调度器
    /// <summary>
    /// QuartzJobCenterService
    /// </summary>
    [DependsOn(typeof(IQuartzJobCenterService))]
    public class QuartzJobCenterService : IQuartzJobCenterService
    {
        #region Field
        private IScheduler _scheduler;
        private readonly ISchedulerFactory _schedulerFactory;

        private readonly ILog _log;
        #endregion

        #region Ctor
        public QuartzJobCenterService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;

            _log = LogManager.GetLogger(typeof(QuartzJobCenterService));
        }
        #endregion

        #region Public Method
        /// <summary>
        /// StartJobAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> StartJobAsync<T>() where T : IQuartzJob
        {
            try
            {
                _scheduler = await _schedulerFactory.GetScheduler();

                //开启调度器
                if (_scheduler.IsStarted)
                {
                    return ServiceResult<string>.IsSuccess(null, $"StartJobAsync Faild: {typeof(T).Name} Aleardy Exists");
                }
                await _scheduler.Start();

                //创建任务
                var jobDetail = JobBuilder.Create<T>()
                                .WithIdentity(typeof(T).Name, typeof(T).Name)
                                .Build();

                // 获取触发器
                ITrigger trigger = typeof(T).GetProperty(nameof(IQuartzJob.Trigger)).GetValue(Activator.CreateInstance<T>()) as ITrigger;

                //将触发器和任务器绑定到调度器中
                await _scheduler.ScheduleJob(jobDetail, trigger);

                var message = $"StartJobAsync Success: {typeof(T).Name}";
                _log.Info(message);
                return ServiceResult<string>.IsSuccess(null, message);
            }
            catch (Exception ex)
            {
                var message = $"StartJobAsync Faild: {typeof(T).Name}";
                _log.Error(message);
                return ServiceResult<string>.IsFailed(null, message, ex);
            }
        }

        // TODO: BUG
        /// <summary>
        /// StopJobAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ServiceResult<string>> StopJobAsync<T>() where T : IQuartzJob
        {
            try
            {

                _scheduler = await _schedulerFactory.GetScheduler();

                //await _scheduler.Shutdown();

                await _scheduler.DeleteJob(JobKey.Create(_scheduler.SchedulerName, _scheduler.SchedulerName));


                var message = $"StopJobAsync Success: {typeof(T).Name}";
                _log.Info(message);
                return ServiceResult<string>.IsSuccess(null, message);
            }
            catch (Exception ex)
            {
                var message = $"StopJobAsync Faild: {typeof(T).Name}";
                _log.Error(message);
                return ServiceResult<string>.IsFailed(null, message, ex);
            }
        }


        /// <summary>
        /// StopAllJobsAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ServiceResult<string>> StopAllJobsAsync()
        {
            try
            {
                IReadOnlyList<IScheduler> _schedulers = await _schedulerFactory.GetAllSchedulers();
                foreach (var scheduler in _schedulers)
                {
                    try
                    {
                        //await scheduler.DeleteJob(JobKey.Create(scheduler.SchedulerName, scheduler.SchedulerName));

                        if (scheduler.IsShutdown)
                        {
                            continue;
                        }
                        await scheduler.Shutdown();
                        _log.Info($"StopJobAsync Success: {scheduler.SchedulerName}");
                    }
                    catch (Exception)
                    {
                        _log.Info($"StopJobAsync Faild: {scheduler.SchedulerName}");
                        throw;
                    }
                }

                return ServiceResult<string>.IsSuccess(null, $"StopAllJobsAsync Success");
            }
            catch (Exception ex)
            {
                var message = $"StopAllJobsAsync Faild";
                _log.Error(message);
                return ServiceResult<string>.IsFailed(null, message, ex);
            }
        }

        #endregion

    }
}
