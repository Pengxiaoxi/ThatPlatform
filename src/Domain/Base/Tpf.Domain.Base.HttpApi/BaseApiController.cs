using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tpf.Common.ResponseExtensions.ServiceResult;

namespace Tpf.Domain.Base.HttpApi
{
    /// <summary>
    /// BaseApiController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BaseApiController
    {
        #region Field
        //protected readonly ILogger<object> _log;
        #endregion

        #region Ctor
        public BaseApiController()
        {

        }

        #endregion

        #region Response
        public ServiceResult<T> Success<T>(T data = default, string? message = default)
        {
            return ServiceResult<T>.IsSuccess(data, message);
        }

        public ServiceResult<T> Failed<T>(T data = default, string? message = default)
        {
            return ServiceResult<T>.IsFailed(data, message);
        }

        public ServiceResult<T> Failed<T>(string? message = default)
        {
            return ServiceResult<T>.IsFailed(default, message);
        }
        #endregion


    }
}
