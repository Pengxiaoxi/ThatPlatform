using Tpf.Common.CommonAttributes;
using Tpf.Domain.AuthInfo.HttpApi;

namespace Tpf.Auth.Api.Module
{
    /// <summary>
    /// AuthApiAppModule
    /// </summary>
    [DependsOn(
        typeof(AuthInfoHttpApiModule)
        )]
    public class AuthApiAppModule
    {
    }
}
