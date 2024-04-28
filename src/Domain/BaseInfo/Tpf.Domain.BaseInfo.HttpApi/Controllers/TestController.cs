using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.Base.Domain.Context;
using Tpf.Domain.Base.HttpApi;
using Tpf.Domain.Common.RestApplication.Auth;

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
        public async Task<Result<UserContextInfo>> GetCurrentUserInfoByRefit()
        {
            var result = await _authRestSerivce.GetCurrentUserInfo();

            
            return result;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<Result<bool>> Test()
        {
            var taskList = new List<Task<bool>>();
            //await Task.WhenAll(taskList);

            taskList.Add(ActionA());
            taskList.Add(ActionB());

            return Result<bool>.IsSuccess(true);
        }

        private async Task<bool> ActionA()
        {
            Console.WriteLine("Task A");
            return await Task.FromResult(true);
        }

        private async Task<bool> ActionB()
        {
            Console.WriteLine("Task B");
            return await Task.FromResult(true);
        }
    }
}
