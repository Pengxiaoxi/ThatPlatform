using Autofac;
using Microsoft.Extensions.Logging;
using Tpf.Domain.Base.Application;
using Tpf.Domain.Base.Application.Contacts;
using Tpf.IOC;

namespace Tpf.Core.Api.DI
{
    /// <summary>
    /// 
    /// </summary>
    public class DIModule : DependencyInjectionModule
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
