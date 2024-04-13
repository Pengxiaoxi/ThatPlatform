using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tpf.Domain.AuthInfo.Applciation.Svc;
using Tpf.Domain.Base.HttpApi;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.Base.Domain.Context;

namespace Tpf.Domain.AuthInfo.HttpApi.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserInfoController : BaseApiController
    {
        #region Field
        private readonly ILogger<UserInfoController> _logger;
        private readonly IUserService _userService;
        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        public UserInfoController(ILogger<UserInfoController> log
            , IUserService userService
            )
        {
            _logger = log;
            _userService = userService;
        }

        /// <summary>
        /// 1、GetList
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResult<List<UserInfoOutputDto>>> GetList([FromQuery] UserInfoQueryDto query)
        {
            var result = await _userService.GetUserInfoList(query);

            return ServiceResult<List<UserInfoOutputDto>>.IsSuccess(result);
        }

        /// <summary>
        /// 3、Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<bool>> Save(UserInfo model)
        {
            var result = await _userService.Save(model);

            return Success(result);
        }

        /// <summary>
        /// 4、Delete
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<bool>> Delete([FromBody] string id)
        {
            //var result = await _userService.DeleteAsync(x => ids.Contains(x.Id)); 

            var result = await _userService.DeleteAsync(x => x.Id == id);

            return Success(result);
        }

        /// <summary>
        /// 100、获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResult<UserContextInfo>> GetCurrentUserInfo()
        {
            var result = await _userService.GetCurrentUserInfo();

            return Success(result);
        }

    }
}
