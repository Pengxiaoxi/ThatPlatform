using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tpf.Utils;

namespace Tpf.Jobs.QuartzNet.Jobs
{
    /// <summary>
    /// MongoDBAutoBackupJob
    /// </summary>
    public class MongoDBAutoBackupJob : IQuartzJob
    {
        #region Field
        //public ITrigger _trigger => this.GetTrigger();

        public ITrigger Trigger { get => this.GetTrigger(); }
        #endregion

        #region Ctor
        public MongoDBAutoBackupJob()
        {
            //_trigger = this.GetTrigger();
        }
        #endregion

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(async () => {
                await BackupDB();
            });
        }

        public async Task BackupDB()
        {
            //Thread.Sleep(1000 * 10);
            Console.WriteLine($"自动备份MongoDB数据库...");
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取Trigger
        /// </summary>
        /// <returns></returns>
        public ITrigger GetTrigger()
        {
            //创建一个触发器（0 15 10 ? * * 每天的10:15执行）
            //var mongoBackCronConfig = "0/5 * * * * ? ";
            var mongoBackCron = ConfigHelper.GetConfig("JobConfig:MongoDBBackupCron");
            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(mongoBackCron)
                //.WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//每两秒执行一次
                .Build();
            return trigger;
        }
    }
}
