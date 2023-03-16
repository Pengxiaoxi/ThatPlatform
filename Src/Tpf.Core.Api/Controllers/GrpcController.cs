using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tpf.BaseInfo.Applciation.Svc;
using Tpf.BaseInfo.Domain.Entity;
using Tpf.Common.BaseWebApi;
using Tpf.Utils.DevExtensions.ServiceResult;

namespace Tpf.Core.Web.Controllers
{
    /// <summary>
    /// GrpcController
    /// </summary>
    public class GrpcController : BaseApiController
    {

        protected IUserService<UserInfo> _userService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService"></param>
        public GrpcController(ILogger<GrpcController> log
            , IUserService<UserInfo> userService)
            : base(log)
        {
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
