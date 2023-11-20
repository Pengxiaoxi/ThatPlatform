using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tpf.BaseInfo.Applciation.Dto;
using Tpf.BaseInfo.Applciation.Svc;
using Tpf.BaseInfo.Domain.Entity;
using Tpf.Common.BaseWebApi;
using Tpf.Utils.DevExtensions.ServiceResult;

namespace Tpf.Core.Api.Controllers
{
    public class UserController : BaseApiController
    {
        protected IUserService<UserInfo> _userService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(ILogger<UserController> log
            , IUserService<UserInfo> userService)
            : base(log)
        {
            _userService = userService;
        }

        [HttpPost]
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
                Pass = Guid.NewGuid().ToString(),
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

        #region About Login
        [HttpPost]
        public async Task<ServiceResult<LoginOutputDto>> Login(LoginInputDto loginDto)
        {
            var user = await _userService.FindOneAsync(x => x.Account == loginDto.Account);
            if (user is null)
            {
                throw new Exception("User not exist");
            }
            if (user.Pass != loginDto.Pass)
            {
                throw new Exception("Login error, Pass error");
            }

            var result = new LoginOutputDto() { Account = user.Account, UserName = user.UserName };
            return ServiceResult<LoginOutputDto>.IsSuccess(result);
        }
        #endregion


    }
}
