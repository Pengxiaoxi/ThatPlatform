using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tpf.Domain.Base.HttpApi;

namespace Tpf.Ugly.Api.Controllers
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
        public async Task<object> GetTest()
        {
            var result = new { code = 200, msg = "", success = true, data = new object() };

            return await Task.FromResult(result);
        }
    }
}
