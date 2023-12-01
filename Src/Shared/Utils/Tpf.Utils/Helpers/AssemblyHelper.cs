using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tpf.Utils.Helpers
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 当前运行路径下所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetSolutionAssemblies()
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));

            return assemblies.ToArray();
        }


    }
}
