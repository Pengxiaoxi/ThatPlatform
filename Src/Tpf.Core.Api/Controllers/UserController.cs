using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.BaseInfo.Applciation.Dto;
using Tpf.BaseInfo.Applciation.Svc;
using Tpf.BaseInfo.Domain.Entity;
using Tpf.Common.BaseWebApi;
using Tpf.Core.DevExtensions.ServiceResult;

namespace Tpf.Core.Web.Controllers
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

        #region TEST
        [HttpGet]
        public void WriteTest()
        {
            var _textWriter = new StringWriter();
            var subType = BsonBinarySubType.Binary;

            var size = 21 * 1024 * 1024;

            var maxSize = 20 * 1024 * 1024;

            var bytes = Encoding.Default.GetBytes(new string('A', size));

            var format = "{{ \"$binary\" : \"{0}\", \"$type\" : \"{1}\" }}";


            _textWriter.Write(format, Convert.ToBase64String(bytes), ((int)subType).ToString("x2"));

            //var base64Str = Convert.ToBase64String(bytes);
            //var bigStr = string.Format(format, base64Str, ((int)subType).ToString("x2"));
            //if (base64Str.Length < maxSize)
            //{
            //    _textWriter.Write(bigStr);
            //}
            //else
            //{
            //    var batchNum = bigStr.Length % maxSize > 0
            //        ? (bigStr.Length / maxSize) + 1
            //        : bigStr.Length / maxSize;
            //    for (int i = 0; i < batchNum; i++)
            //    {
            //        var writeLegth = bigStr.Length > maxSize ? maxSize : bigStr.Length;
            //        _textWriter.Write(bigStr.Substring(0, writeLegth));
            //        bigStr = bigStr.Remove(0, writeLegth);
            //    }
            //}

            //System.IO.File.WriteAllText(@"C:\Users\pengx\Desktop\before.text", _textWriter.ToString());

            _textWriter.Flush();

        }
        #endregion
    }
}
