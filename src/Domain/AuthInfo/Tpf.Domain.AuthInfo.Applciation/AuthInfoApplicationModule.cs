using Tpf.Common.CommonAttributes;
using Tpf.Common.Module;
using Tpf.Grpc.Client;

namespace Tpf.Domain.AuthInfo.Applciation
{
    [DependsOn(
        typeof(GrpcClientModule)
        )]
    public class AuthInfoApplicationModule : AppModule
    {
    }
}
