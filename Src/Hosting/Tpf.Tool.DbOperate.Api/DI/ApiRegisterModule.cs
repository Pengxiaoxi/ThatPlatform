using Autofac;
using Tpf.Autofac;

namespace Tpf.Tool.DbOperate.Api.DI
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
