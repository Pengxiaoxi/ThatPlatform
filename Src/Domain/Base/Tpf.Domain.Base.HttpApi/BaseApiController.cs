using Microsoft.AspNetCore.Mvc;

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
        //protected readonly ILogger<object> _log;
        #endregion

        #region Ctor
        public BaseApiController()
        {

        }

        #endregion


    }
}
