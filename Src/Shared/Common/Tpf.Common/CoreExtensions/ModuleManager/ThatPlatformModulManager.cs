using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tpf.Common.CommonAttributes;

namespace Tpf.Common.CoreExtensions.ModuleManager
{
    public class ThatPlatformModulManager
    {

        #region Field
        protected readonly ILog _logger;
        #endregion

        #region Ctor
        public ThatPlatformModulManager()
        {

        }

        public ThatPlatformModulManager(ILog logger)
        {
            _logger = logger;
        }
        #endregion

        public List<Type> LoadAllModules()
        {
            //_logger.Debug("Loading Tpf modules...");

            var _modules = FindDependedModules();

            //_logger.Debug($"Found {_modules.Count} Tpf modules in total.");

            return _modules;
            //_logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        public List<Type> FindDependedModules()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //assemblies = assemblies.Append(Assembly.Load("Tpf.Logging.Log4Net")).ToArray();

            assemblies = assemblies.Append(Assembly.Load("Tpf.Jobs.QuartzNet")).ToArray();
            assemblies = assemblies.Append(Assembly.Load("Tpf.Grpc.Client")).ToArray();



            List<Type> _modules = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetCustomAttributes(typeof(DependsOnAttribute), true).Length > 0
                            && x.IsClass
                            && !x.IsAbstract)
                .ToList();
            return _modules;
        }
    }
}
