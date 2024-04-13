using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Log.Log4Net.Svc
{
    [Obsolete]
    public interface ILog4NetLogging<T> : ILogging<T> where T : class
    {

    }
}
