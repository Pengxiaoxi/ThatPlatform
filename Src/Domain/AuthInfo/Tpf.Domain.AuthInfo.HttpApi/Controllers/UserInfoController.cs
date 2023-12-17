using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tpf.Domain.AuthInfo.Applciation.Svc;
using Tpf.Domain.Base.HttpApi;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Common.ResponseExtensions.ServiceResult;

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

        [HttpGet]
        public async Task<ServiceResult<List<UserInfoOutputDto>>> GetUserList()
        {
            //throw new NotImplementedException();

            //var result = await _userService.GetListAsync(x => x.UserName != null);
            var result = await _userService.GetUserInfoList();
            return ServiceResult<List<UserInfoOutputDto>>.IsSuccess(result);
        }

        [HttpPost]
        public async Task<ServiceResult> Insert()
        {
            var userInfo = new UserInfo()
            {
                UserName = "pxx",
                Password = Guid.NewGuid().ToString(),
            };
            await _userService.InsertAsync(userInfo);

            return ServiceResult.IsSuccess("Insert Success");
        }

        [HttpPost]
        public async Task<ServiceResult> Update()
        {
            var users = await _userService.GetListAsync(x => x.UserName != null);
            users.FirstOrDefault().UserName = $"pxx_{new Random().Next(1, int.MaxValue)}";
            await _userService.UpdateAsync(users.FirstOrDefault());

            return ServiceResult.IsSuccess("Update Success");
        }

        [HttpPost]
        public async Task<ServiceResult> Delete()
        {
            var deleteUsers = await _userService.GetListAsync(x => x.UserName != null);
            await _userService.DeleteAsync(deleteUsers.FirstOrDefault());

            return ServiceResult.IsSuccess("Delete Success");
        }

    }
}
