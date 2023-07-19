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
    public class ServiceResult<T> : ServiceResult where T : class
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public static ServiceResult<T> IsSuccess(T data = null, string message = "")
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
        public static ServiceResult<T> IsFailed(T data = null, string message = "", Exception exception = null)
        {
            return new ServiceResult<T>()
            {
                Message = $"{message},Exception: {exception?.Message}, StackTrace: {exception?.StackTrace}",
                Code = ServiceResultCodeEnum.Failed,
            };
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public static ServiceResult<T> IsFailed(T data = null, string message = "")
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
                innerResult.Data = null;
            }
            else
            {
                innerResult.Message = message;
                innerResult.Data = data;
            }

            return innerResult;
        }
    }
}
