using Microsoft.AspNetCore.Mvc;
using Tpf.Security;
using Tpf.Utils;

namespace Tpf.Domain.Base.HttpApi.Controllers
{
    /// <summary>
    /// 加解密接口
    /// </summary>
    //[AllowAnonymous]
    public class SecurityController : BaseApiController
    {
        /// <summary>
        /// 1、AESEncrypt
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> AESEncrypt([FromBody] string content)
        {
            var result = AESHelper.Encrypt(content, ConfigHelper.GetSecurityKey16());

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 2、AESDecrypt
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> AESDecrypt([FromBody] string content)
        {
            var result = AESHelper.Decrypt(content, ConfigHelper.GetSecurityKey16());

            return await Task.FromResult(result);
        }

    }
}
