using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tpf.Domain.Base.HttpApi;
using Tpf.Utils;

namespace Tpf.Platform.Api.Controllers
{
    /// <summary>
    /// Redis Test
    /// </summary>
    [AllowAnonymous]
    public class RedisTestController : BaseApiController
    {
        private const string ListKey = "my_list";

        /// <summary>
        /// Redis List Test
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> ListRightPop()
        {
            Task.Run(async () =>
            {
                int count = 0;
                while (true) 
                {
                    //Thread.Sleep(1 * 1000);

                    var rightValue = await RedisHelper.RPopAsync(ListKey);

                    if (!string.IsNullOrEmpty(rightValue))
                    {
                        Console.WriteLine(rightValue);

                        count++;

                        ConsoleHelper.WriteSuccessLine($"总计：{count}");
                    }

                };
            });

            return true;
        }

    }
}
