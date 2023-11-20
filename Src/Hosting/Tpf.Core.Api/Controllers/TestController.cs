using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tpf.Common.BaseWebApi;

namespace Tpf.Core.Api.Controllers
{
    /// <summary>
    /// TestController
    /// </summary>
    public class TestController : BaseApiController
    {
        #region Field

        #endregion

        #region Ctor
        public TestController(ILogger<TestController> log
            )
            : base(log)
        {

        }
        #endregion

        [HttpGet]
        public async Task<object> GetTest()
        {
            // 日志示例
            _log.LogInformation("This is a log message !", "");

            _log.LogWarning("This is a log message !", "");
            _log.LogError("This is a log message !", "");

            //立即执行一次 不执行
            timer.Change(0, Timeout.Infinite);
            //Console.ReadKey();

            var result = new { code = 200, msg = "", success = true, data = new object() };
            return result;
        }

        [HttpGet]
        public async Task<string> TestRgex(string scriptText)
        {
            ////立即执行一次 不执行
            //timer.Change(0, Timeout.Infinite);
            //Console.ReadKey();

            //var result = new { code = 200, msg = "", success = true, data = new object() };
            //return result;
            Console.WriteLine();
            Console.WriteLine();

            await Regex(scriptText);

            return "Ok";
        }

        [HttpGet]
        public async Task<string> TestSqlSugar()
        {
            return await Task.FromResult(string.Empty);
        }

        #region Private

        //构建 Timer
        static Timer timer = new Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);

        static void TimerCallBack(object state)
        {
            var nextTime = DateTime.Now.AddSeconds(10);
            Console.WriteLine("{0} 执行一次,下次执行时间 {1}", DateTime.Now, nextTime);
            //执行完后,重新设置定时器下次执行时间.
            //timer.Change(nextTime.Subtract(DateTime.Now), Timeout.InfiniteTimeSpan);
        }


        private async Task Regex(string scriptText)
        {
            // Cost 6306ms
            //var regex = new Regex("(?<!:)\\/\\/.*|\\/\\*(\\s|.)*?\\*\\/", RegexOptions.Compiled);
            //var scriptTextOutput = regex.Replace(scriptText, "");

            // Cost 17284ms
            //var scriptTextOutput = new Regex("(?<!:)\\/\\/.*|\\/\\*(\\s|.)*?\\*\\/").Replace(scriptText, "");

            // Cost 17284ms
            //var regex = new Regex("(?<!:)\\/\\/.*|\\/\\*(\\s|.)*?\\*\\/", RegexOptions.Compiled);
            var regex = new Regex("(?<!:)\\/\\/.*|\\/\\*(\\s|.)*?\\*\\/", RegexOptions.None);
            var scriptTextOutput = regex.Replace(scriptText, "");


            Console.WriteLine("scriptTextOutput");
            Console.WriteLine(scriptTextOutput);

        }

        #endregion
    }
}
