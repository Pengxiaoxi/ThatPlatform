using Microsoft.AspNetCore.Mvc;
using Tpf.Common.BaseWebApi;

namespace Tpf.BaseInfo.HttpApi
{
    public class BaseInfoController : BaseApiController
    {
        [HttpGet]
        public async Task<Object> GetBaseInfoList()
        {
            var result = new { code = 200, msg = "", success = true, data = new object() };

            return await Task.FromResult(result);
        }
    }
}
