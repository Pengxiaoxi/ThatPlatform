using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Common.BaseORM.EFCore
{
    public interface IEFCoreRepository<T> : IBaseRepository<T> where T : class
    {
        
    }
}
