using Tpf.Common.CommonAttributes;
using Tpf.Common.Module;
using Tpf.Domain.AuthInfo.HttpApi;

namespace Tpf.Core.Api
{
    /// <summary>
    /// TpfCoreApiModule
    /// </summary>
    [DependsOn(
        typeof(AuthInfoHttpApiModule)
        )]
    public class TpfCoreApiModule : AppModule
    {
    }
}
