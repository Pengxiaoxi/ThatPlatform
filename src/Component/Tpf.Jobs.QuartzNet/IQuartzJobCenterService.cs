using System.Threading.Tasks;
using Tpf.Common.ResponseExtensions.ServiceResult;

namespace Tpf.Jobs.QuartzNet
{
    public interface IQuartzJobCenterService
    {
        /// <summary>
        /// 开启任务
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> StartJobAsync<T>() where T : IQuartzJob;

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> StopJobAsync<T>() where T : IQuartzJob;

        /// <summary>
        /// StopAllJobsAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ServiceResult<string>> StopAllJobsAsync();

    }
}
