using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Domain.Base.Repository
{
    public interface IEFCoreRepository<T> : IBaseRepository<T> where T : class
    {
        
    }
}
