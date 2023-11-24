using Tpf.Common.CommonAttributes;
using Tpf.Domain.AuthInfo.Applciation;

namespace Tpf.Domain.AuthInfo.HttpApi
{
    [DependsOn(
        typeof(AuthInfoApplicationModule
        ))]
    public class AuthInfoHttpApiModule
    {
    }
}
