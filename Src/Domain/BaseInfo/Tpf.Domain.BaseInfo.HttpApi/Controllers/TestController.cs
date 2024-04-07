using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.Base.Domain.Context;
using Tpf.Domain.Base.HttpApi;
using Tpf.Domain.Common.RestApplication;

namespace Tpf.Domain.BaseInfo.HttpApi.Controllers
{
    public class TestController: BaseApiController
    {
        private readonly IAuthRestSerivce _authRestSerivce;

        public TestController(IAuthRestSerivce authSerivce)
        {
            _authRestSerivce = authSerivce;
        }

        /// <summary>
        /// GetCurrentUserInfoByRefit
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResult<UserContextInfo>> GetCurrentUserInfoByRefit()
        {
            var result = await _authRestSerivce.GetCurrentUserInfo();

            return result;
        }


    }
}
