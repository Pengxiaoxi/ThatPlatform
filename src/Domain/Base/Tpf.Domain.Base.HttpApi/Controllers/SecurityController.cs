using Microsoft.AspNetCore.Mvc;
using Tpf.Security;
using Tpf.Utils;

namespace Tpf.Domain.Base.HttpApi.Controllers
{
    /// <summary>
    /// 加解密接口
    /// </summary>
    //[AllowAnonymous]
    //[Route("api/security/[action]")]
    public class SecurityController : BaseApiController
    {
        /// <summary>
        /// 1、AESEncrypt
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        //[ActionName("encryptByAES")]
        public async Task<string> EncryptByAES([FromBody] string content)
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
        //[ActionName("decryptByAES")]
        public async Task<string> DecryptByAES([FromBody] string content)
        {
            var result = AESHelper.Decrypt(content, ConfigHelper.GetSecurityKey16());

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 3、GetSecurityKey16
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityKey16()
        {
            return await Task.FromResult(ConfigHelper.GetSecurityKey16());
        }

        /// <summary>
        /// 4、GetSecurityKey32
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetSecurityKey32()
        {
            return await Task.FromResult(ConfigHelper.GetSecurityKey32());
        }

    }
}
