using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Domain.AuthInfo.Applciation.Svc;
using Tpf.Domain.Base.HttpApi;

namespace Tpf.Domain.AuthInfo.HttpApi.Controllers
{
    /// <summary>
    /// Authentication
    /// </summary>
    [AllowAnonymous]
    public class AuthenticationController : BaseApiController
    {
        #region Field
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;
        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        public AuthenticationController(ILogger<AuthenticationController> log
            , IUserService userService
            )
        {
            _logger = log;
            _userService = userService;
        }


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
    }
}
