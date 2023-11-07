using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Utils.DevExtensions.ServiceResult
{
    /// <summary>
    /// ServiceResult
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty("code")]
        public ServiceResultCodeEnum Code { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success => Code == ServiceResultCodeEnum.Succeed;

        /// <summary>
        /// 时间戳(毫秒)
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();


        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServiceResult IsSuccess(string message = "")
        {
            return new ServiceResult()
            {
                Message = message,
                Code = ServiceResultCodeEnum.Succeed
            };
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServiceResult IsFailed(string message = "", Exception exception = null)
        {
            return new ServiceResult()
            {
                //Message = $"{message},Exception: {exception?.Message}, StackTrace: {exception?.StackTrace}",
                Message = message,
                Code = ServiceResultCodeEnum.Failed
            };
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="exexception></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServiceResult IsFailed(Exception exception)
        {
            return new ServiceResult()
            {
                Message = exception.InnerException?.StackTrace,
                Code = ServiceResultCodeEnum.Failed
            };
        }
    }
}
