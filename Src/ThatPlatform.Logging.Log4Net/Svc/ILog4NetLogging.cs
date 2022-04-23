using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Logging.Log4Net
{
    public interface ILog4NetLogging<T> : ILogging<T> where T : class
    {

    }
}
