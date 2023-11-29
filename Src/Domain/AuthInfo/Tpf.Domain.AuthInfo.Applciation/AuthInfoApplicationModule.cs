using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.Common.CommonAttributes;
using Tpf.Grpc.Client;

namespace Tpf.Domain.AuthInfo.Applciation
{
    [DependsOn(
        typeof(GrpcClientModule)
        )]
    public class AuthInfoApplicationModule
    {
    }
}
