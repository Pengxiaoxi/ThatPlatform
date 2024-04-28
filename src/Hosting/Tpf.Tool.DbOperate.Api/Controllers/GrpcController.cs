using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.AuthInfo.Applciation.Svc;
using Tpf.Domain.Base.HttpApi;

namespace Tpf.Tool.DbOperate.Api.Controllers
{
    /// <summary>
    /// GrpcController
    /// </summary>
    public class GrpcController : BaseApiController
    {
        #region Field
        private readonly ILogger<GrpcController> _log;
        protected readonly IUserService _userService;
        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService"></param>
        public GrpcController(ILogger<GrpcController> log
            , IUserService userService)
        {
            _log = log;
            _userService = userService;
        }

        /// <summary>
        /// gRpc Example: GetOrgByUserByGrpcExample
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<object>> GetOrgByUserByGrpcExample()
        {
            var result = await _userService.GetOrgByUserByGrpc();
            return Result<object>.IsSuccess(result);
        }

    }
}
