using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Logging.Log4Net.Impl
{
    public class Log4NetLogging<T> : ILog4NetLogging<T> where T : class
    {
        private readonly ILog _log;

        public Log4NetLogging()
        {
            _log = LogManager.GetLogger(typeof(T));
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }
    }
}
