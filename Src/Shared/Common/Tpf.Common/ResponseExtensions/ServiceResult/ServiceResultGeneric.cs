using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Common.ResponseExtensions.ServiceResult
{
    /// <summary>
    /// ServiceResult
    /// </summary>
    public partial class ServiceResult<T> // : ServiceResult
    {
        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public static ServiceResult<T> IsSuccess(T data = default, string message = "")
        {
            return new ServiceResult<T>()
            {
                Message = message,
                Code = ServiceResultCodeEnum.Succeed,
                Data = data
            };
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public static ServiceResult<T> IsFailed(string message, Exception exception = null)
        {
            return new ServiceResult<T>()
            {
                Message = $"Exception: {exception?.Message}",
                Code = ServiceResultCodeEnum.Failed,
            };
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public static ServiceResult<T> IsFailed(T data = default, string message = "")
        {
            var innerResult = new ServiceResult<T>()
            {
                Code = ServiceResultCodeEnum.Failed
            };

            //此处判断是为了避免错误的使用重载方法
            if (data != null && data.GetType() == typeof(string)
                && string.IsNullOrWhiteSpace(message))
            {
                innerResult.Message = data as string;
                innerResult.Data = default;
            }
            else
            {
                innerResult.Message = message;
                innerResult.Data = data;
            }

            return innerResult;
        }
    }

    public partial class ServiceResult<T>
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        // <summary>
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

    }
}
