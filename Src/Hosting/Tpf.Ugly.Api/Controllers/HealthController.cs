using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tpf.Common.BaseWebApi;

namespace Tpf.Ugly.Web.Controllers
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
            var result = new { code = 200, msg = "", success = true, data = new object() };

            return await Task.FromResult(result);
        }
    }
}
