using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Logging
{
    public interface ILog4NetLogging :ILogging
    {
        void Info(object message, Exception exception);

        void Debug(object message, Exception exception);

        void Error(object message, Exception exception);

    }
}
