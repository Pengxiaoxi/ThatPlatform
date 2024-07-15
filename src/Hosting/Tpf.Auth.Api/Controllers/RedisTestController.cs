using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Tpf.Domain.Base.HttpApi;

namespace Tpf.Auth.Api.Controllers
{
    /// <summary>
    /// Redis Test
    /// </summary>
    [AllowAnonymous]
    public class RedisTestController : BaseApiController
    {
        private const string ListKey = "my_list";


        [HttpPost]
        public async Task<bool> ListLeftPush([FromBody] string value)
        {
            //Task.Run(async () =>
            //{
            //    for (int i = 1; i <= 100; i++)
            //    {
            //        await RedisHelper.LPushAsync(ListKey, $"{i}");

            //        Console.WriteLine(i);

            //        //Thread.Sleep(1 * 1000);
            //    }
            //});


            for (int i = 1; i <= 100; i++)
            {
                await RedisHelper.LPushAsync(ListKey, $"{i}");

                Console.WriteLine(i);

                //Thread.Sleep(1 * 1000);
            }

            //var conList = new ConcurrentBag<int>();
            //for (int i = 1; i <= 100; i++)
            //{
            //    conList.Add(i);
            //}

            //Parallel.ForEach(conList, new ParallelOptions() { MaxDegreeOfParallelism = 5 }, async (x) => 
            //{
            //    await RedisHelper.LPushAsync(ListKey, $"{x}");

            //    Console.WriteLine(x);
            //});

            return true;
        }
    }
}
