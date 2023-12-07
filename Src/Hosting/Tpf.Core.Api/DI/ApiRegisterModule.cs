using Autofac;
using Tpf.Autofac;

namespace Tpf.Core.Api.DI
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiRegisterModule : AutofacRegisterModule
    {
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // 日志
            //builder.RegisterType(typeof(ILogger<>)).As(typeof(Logger<>)).SingleInstance();

        }
    }
}
