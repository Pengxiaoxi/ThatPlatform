using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ThatPlatform.Common.BaseWebApi;

namespace ThatPlatform.Core.Web.Controllers
{
    /// <summary>
    /// HealthController
    /// </summary>
    public class HealthController : BaseApiController
    {
        public HealthController()
        {


        }

        [HttpGet]
        public async Task<Object> GetTest()
        {
            var result = new { code = 200, msg = "", isSucess = true, data = new object() };

            return await Task.FromResult(result);
        }    
    }
}