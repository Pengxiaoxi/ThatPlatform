using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Tpf.Utils.AssemblyHelpers
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 当前运行路径下所有程序集
        /// TODO: 防 DLL 注入安全问题
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetSolutionAssemblies()
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(x => AssemblyLoadContext.Default.LoadFromAssemblyName(AssemblyName.GetAssemblyName(x)));

            return assemblies.ToArray();
        }


    }
}
