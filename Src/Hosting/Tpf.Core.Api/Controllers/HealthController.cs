using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tpf.Domain.Base.HttpApi;

namespace Tpf.Core.Api.Controllers
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
        public async Task<object> GetTest()
        {
            var result = new { code = 200, msg = "", success = true, data = new object() };

            return await Task.FromResult(result);
        }
    }
}
