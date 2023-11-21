using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.Base.HttpApi;
using Tpf.Domain.UserInfo.Applciation.Svc;
using Tpf.Utils.DevExtensions.ServiceResult;

namespace Tpf.Core.Api.Controllers
{
    /// <summary>
    /// GrpcController
    /// </summary>
    public class GrpcController : BaseApiController
    {
        #region Field
        private readonly ILogger<GrpcController> _log;
        protected readonly IUserService<UserInfo> _userService; 
        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService"></param>
        public GrpcController(ILogger<GrpcController> log
            , IUserService<UserInfo> userService)
        {
            _log = log;
            _userService = userService;
        }

        /// <summary>
        /// gRpc Example: GetOrgByUserByGrpcExample
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<object>> GetOrgByUserByGrpcExample()
        {
            var result = await _userService.GetOrgByUserByGrpc();
            return ServiceResult<object>.IsSuccess(result);
        }

    }
}
