using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.BaseWebApi;
using ThatPlatform.Infrastructure.DevExtensions.ServiceResult;

namespace ThatPlatform.Core.Web.Controllers
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
        public GrpcController(IUserService<UserInfo> userService)
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
