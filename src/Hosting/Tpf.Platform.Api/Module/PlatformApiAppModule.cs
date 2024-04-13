using Tpf.Common.CommonAttributes;
using Tpf.Domain.BaseInfo.HttpApi;

namespace Tpf.Platform.Api.Module
{
    /// <summary>
    /// PlatformApiAppModule
    /// </summary>
    [DependsOn(
        typeof(BaseInfoHttpApiModule)
        )]
    public class PlatformApiAppModule
    {
    }
}
