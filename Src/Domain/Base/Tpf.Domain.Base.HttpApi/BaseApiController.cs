using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tpf.Domain.Base.HttpApi
{
    /// <summary>
    /// BaseApiController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class BaseApiController
    {
        #region Field
        protected readonly ILogger<object> _log;
        #endregion

        #region Ctor
        public BaseApiController()
        {

        }

        public BaseApiController(ILogger<object> log)
        {
            _log = log; //_log = LogManager.GetLogger(typeof(T));


        }
        #endregion


    }
}
