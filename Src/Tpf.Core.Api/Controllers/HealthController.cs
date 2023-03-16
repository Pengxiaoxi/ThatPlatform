using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tpf.Common.BaseWebApi;

namespace Tpf.Core.Web.Controllers
{
    /// <summary>
    /// HealthController
    /// </summary>
    public class HealthController : BaseApiController
    {
        public HealthController(ILogger<HealthController> log
            )
            : base(log)
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
