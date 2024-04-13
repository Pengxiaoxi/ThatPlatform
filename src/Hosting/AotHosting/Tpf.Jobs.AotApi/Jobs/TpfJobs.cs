using Hangfire;

namespace Tpf.Jobs.AotApi.Jobs
{
    public class TpfJobs
    {
        public async Task Do()
        {
            BackgroundJob.Enqueue(() => DoBackgroundJobJob());

            RecurringJob.AddOrUpdate(() => DoRecurringJob(), Cron.Minutely);

            await Task.CompletedTask;
        }

        public void DoBackgroundJobJob()
        {
            Console.WriteLine("Hangfire: fire-and-forget job");
        }

        public void DoRecurringJob()
        {
            Console.WriteLine("Hangfire: Recurring Job, Hello, world!");
        }

    }
}
