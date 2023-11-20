using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tpf.Core.Api.Dto
{
    /// <summary>
    /// BaseTCCloudDBDto 固定公共参数
    /// </summary>
    public class BaseTCCloudDBRequestDto
    {
        /// <summary>
        ///【必选】公共参数：接口标识
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        ///【必选】公共参数：版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///【必选】公共参数：区域
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        ///【必选】实例ID
        /// </summary>
        public string InstanceId { get; set; }

    }

    /// <summary>
    /// 返回参数Base
    /// </summary>
    public class BaseTCCloudDBResponseDTO
    {
        /// <summary>
        /// 唯一请求 ID，每次请求都会返回。定位问题时需要提供该次请求的 RequestId
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Response错误信息
        /// </summary>
        public ResponseError Error { get; set; }
    }

    /// <summary>
    /// 返回错误信息
    /// </summary>
    public class ResponseError
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    /// 统一的返回对象Base结构体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T> where T : BaseTCCloudDBResponseDTO
    {
        public T Response { get; set; }
    }
}
