﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Shared.Logging.Log4Net
{
    [Obsolete]
    public interface ILogging<T> where T : class
    {
        void Info(object message);

        void Debug(object message);

        void Warn(object message);

        void Error(object message);

        void Fatal(object message);

    }
}
