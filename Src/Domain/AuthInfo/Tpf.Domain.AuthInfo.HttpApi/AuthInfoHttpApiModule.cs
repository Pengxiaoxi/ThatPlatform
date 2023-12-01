using Tpf.Common.CommonAttributes;
using Tpf.Domain.AuthInfo.Applciation;
using Tpf.Domain.Base.HttpApi;

namespace Tpf.Domain.AuthInfo.HttpApi
{
    [DependsOn(
        typeof(AuthInfoApplicationModule),
        typeof(DomainBaseApiModule)
        )]
    public class AuthInfoHttpApiModule
    {

    }
}
