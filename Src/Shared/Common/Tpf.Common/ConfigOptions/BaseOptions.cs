using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Common.ConfigOptions
{
    public abstract class BaseOptions
    {
        public abstract string GetOptionsName();

    }

    public interface IBaseOptions
    {
        //public string CurrentOptionsName { get; }

        //string CurrentOptionsName();

    }

}
