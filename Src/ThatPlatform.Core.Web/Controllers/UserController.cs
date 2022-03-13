using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.BaseWebApi;

namespace ThatPlatform.Core.Web.Controllers
{
    public class UserController : BaseApiController
    {
        protected readonly IUserService _userService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<List<UserInfo>> GetUserInfoList()
        {
            return await _userService.GetUserInfosAsync();
        }

        [HttpPost]
        public async Task Insert()
        {
            var userInfo = new UserInfo()
            {
                UserName = "pxx",
                UserPass = Guid.NewGuid().ToString(),
            };
            await _userService.InsertAsync(userInfo);
        }
    }
}
