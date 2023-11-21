using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tpf.Domain.Base.HttpApi;
using Tpf.Jobs.QuartzNet;
using Tpf.Jobs.QuartzNet.Jobs;
using Tpf.Utils.DevExtensions.ServiceResult;

namespace Tpf.Core.Api.Controllers
{
    /// <summary>
    /// JobController
    /// </summary>
    public class JobController : BaseApiController
    {
        #region Field
        private readonly IQuartzJobCenterService _quartzJobCenterService;
        #endregion

        #region Ctor
        public JobController(IQuartzJobCenterService quartzJobCenterService
            )
        {
            _quartzJobCenterService = quartzJobCenterService;
        }
        #endregion

        /// <summary>
        /// StartMongoDBBackupJob
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<string>> StartMongoDBBackupJob()
        {
            var result = await _quartzJobCenterService.StartJobAsync<MongoDBAutoBackupJob>();
            return result;
        }

        /// <summary>
        /// StopMongoDBBackupJob
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<string>> StopMongoDBBackupJob()
        {
            var result = await _quartzJobCenterService.StopJobAsync<MongoDBAutoBackupJob>();
            return result;
        }

        /// <summary>
        /// StopAllJobsAsync
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<string>> StopAllJobsAsync()
        {
            var result = await _quartzJobCenterService.StopAllJobsAsync();
            return result;
        }

    }
}
