using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThatPlatform.Common.BaseWebApi;
using ThatPlatform.Infrastructure.DevExtensions.ServiceResult;
using ThatPlatform.Jobs.QuartzNet;
using ThatPlatform.Jobs.QuartzNet.Jobs;

namespace ThatPlatform.Core.Web.Controllers
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
        public JobController(IQuartzJobCenterService quartzJobCenterService)
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
