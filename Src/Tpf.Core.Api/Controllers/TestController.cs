using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tpf.Common.BaseWebApi;

namespace Tpf.Core.Web.Controllers
{
    /// <summary>
    /// TestController
    /// </summary>
    public class TestController : BaseApiController
    {
        #region Ctor
        public TestController()
        {

        }
        #endregion

        [HttpGet]
        public async Task<Object> GetTest()
        {
            //立即执行一次 不执行
            timer.Change(0, Timeout.Infinite);
            Console.ReadKey();

            var result = new { code = 200, msg = "", isSucess = true, data = new object() };
            return result;
        }

        [HttpGet]
        public async Task<string> TestRgex(string scriptText)
        {
            ////立即执行一次 不执行
            //timer.Change(0, Timeout.Infinite);
            //Console.ReadKey();

            //var result = new { code = 200, msg = "", isSucess = true, data = new object() };
            //return result;
            Console.WriteLine();
            Console.WriteLine();

            await this.Regex(scriptText);

            return "Ok";
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
