using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Domain.Base.HttpApi.Controllers
{
    /// <summary>
    /// 加解密接口 [AllowAnonymous]
    /// </summary>
    [AllowAnonymous]
    public class SecurityController : BaseApiController
    {

    }
}
