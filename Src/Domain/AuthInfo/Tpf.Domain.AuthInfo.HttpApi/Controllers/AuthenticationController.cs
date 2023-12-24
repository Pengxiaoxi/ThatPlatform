using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tpf.Common.Config;
using Tpf.Common.ResponseExtensions.ServiceResult;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Domain.AuthInfo.Applciation.Svc;
using Tpf.Domain.AuthInfo.Domain.Entity;
using Tpf.Domain.Base.HttpApi;
using Tpf.Utils;
using Tpf.Utils.Security;

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
        private readonly IMapper _mapper;
        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        public AuthenticationController(ILogger<AuthenticationController> log
            , IUserService userService
            , IMapper mapper)
        {
            _logger = log;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// 1 注册
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<ServiceResult<bool>> Register(RegisterDto dto)
        {
            if (dto is null || string.IsNullOrEmpty(dto.Account) || string.IsNullOrEmpty(dto.Password))
            {
                throw new ArgumentNullException("参数错误");
            }

            var isExist = await _userService.AnyAsync(x => x.Account == dto.Account);
            if (isExist)
            {
                return ServiceResult<bool>.IsFailed(false, $"当前账号已存在：{dto.Account}");
            }

            var user = _mapper.Map<UserInfo>(dto);
            user.Secretkey = MD5Helper.MD5Encrypt32($"{user.Password}#{ConfigHelper.Get(AppConfig.Security)}");
            user.Password = GeneratePassBySecretkey(dto.Password, user.Secretkey);
            var result = await _userService.InsertAsync(user);

            return Success(result);
        }

        /// <summary>
        /// 2 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<ServiceResult<LoginOutputDto>> Login(LoginInputDto dto)
        {
            var user = await _userService.GetAsync(x => x.Account == dto.Account);
            if (user is null)
            {
                return Failed<LoginOutputDto>("用户不存在");
            }

            var password = GeneratePassBySecretkey(dto.Password, user.Secretkey);
            if (!user.Password.Equals(password))
            {
                return Failed<LoginOutputDto>("登录失败，密码错误");
            }

            // TODO: generate token
            var result = new LoginOutputDto() { Token = user.Account, RefreshToken = user.UserName };
            return Success(result);
        }

        /// <summary>
        /// 3 注销登录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<bool>> Logout(string account)
        {
            //由于 token 无状态且一次性，注销登录简单做法直接前端清空缓存的 token；
            //但为了避免token泄露被非法一直使用，因此可使用 redis/ db 兜底，注销登录的token加入过期黑名单

            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentNullException("参数错误");
            }

            return Success(true);
        }


        #region Private Method
        private string GeneratePassBySecretkey(string webPass, string secretkey)
        {
            return MD5Helper.MD5Encrypt32($"{webPass}#{secretkey}");
        }

        #endregion

    }
}
