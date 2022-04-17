﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Dto;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.BaseWebApi;
using ThatPlatform.Infrastructure.DevExtensions.ServiceResult;

namespace ThatPlatform.Core.Web.Controllers
{
    public class UserController : BaseApiController
    {
        protected IUserService<UserInfo> _userService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService<UserInfo> userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ServiceResult<List<UserInfo>>> GetUserList()
        {
            //throw new NotImplementedException();

            var result = await _userService.GetListAsync(x => x.UserName != null);
            return ServiceResult<List<UserInfo>>.IsSuccess(result);
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

        #region Grpc
        [HttpPost]
        public async Task<ServiceResult<object>> GetOrgByUserByGrpc()
        {
            var result = await _userService.GetOrgByUserByGrpc();
            return ServiceResult<object>.IsSuccess(result);
        }
        #endregion
    }
}
