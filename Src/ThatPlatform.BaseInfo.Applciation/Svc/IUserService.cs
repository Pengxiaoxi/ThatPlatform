using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.BaseInfo.Domain.Entity;
using ThatPlatform.Common.BaseDomain.Entity;
using ThatPlatform.Common.BaseDomain.Svc;

namespace ThatPlatform.BaseInfo.Applciation.Svc
{
    public interface IUserService<T> : IBaseService<T> where T : BaseEntity<string>
    {

    }
}
