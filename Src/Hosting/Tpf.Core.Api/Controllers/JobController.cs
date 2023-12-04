using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.Base.HttpApi;
using Tpf.Jobs.QuartzNet;
using Tpf.Jobs.QuartzNet.Jobs;

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
