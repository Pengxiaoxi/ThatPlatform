using System;

namespace Tpf.Core.CommonAttributes
{
    /// <summary>
    /// DependsOnAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// Types of depended modules.
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

        /// <summary>
        /// Used to define dependencies of an ABP module to other modules.
        /// </summary>
        /// <param name="dependedModuleTypes">Types of depended modules</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
