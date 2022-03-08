using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.Common.BaseDomain.Svc;

namespace ThatPlatform.Common.BaseDomain.Impl
{
    public class BaseService<T> : IBaseService<T> where T : class
    {

    }
}
