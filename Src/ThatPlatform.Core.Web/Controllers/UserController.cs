using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Applciation.Svc;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.BaseWebApi;

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
        public async Task<List<UserInfo>> GetUserList()
        {
            return await _userService.GetListAsync(x => x.UserName != null);
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

        [HttpPost]
        public async Task Update()
        {
            var users = await _userService.GetListAsync(x => x.UserName != null);
            users.FirstOrDefault().UserName = $"pxx_{new Random().Next(1, int.MaxValue)}";
            await _userService.UpdateAsync(users.FirstOrDefault());
        }

        [HttpPost]
        public async Task Delete()
        {
            var deleteUsers = await _userService.GetListAsync(x => x.UserName != null);
            await _userService.DeleteAsync(deleteUsers.FirstOrDefault());
        }
    }
}
