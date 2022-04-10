using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Common.BaseWebApi
{
    /// <summary>
    /// BaseApiController
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseApiController
    {
        #region Field
        protected readonly ILog _log;
        #endregion

        #region Ctor
        public BaseApiController()
        {
            _log = LogManager.GetLogger(typeof(BaseApiController));
        }
        #endregion

        
    }
}
