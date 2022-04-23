using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Jobs.QuartzNet
{
    public interface IQuartzJob : IJob
    {
        public ITrigger Trigger { get; }

    }
}
