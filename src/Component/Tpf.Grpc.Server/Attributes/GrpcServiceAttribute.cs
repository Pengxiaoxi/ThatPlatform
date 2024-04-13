using System;

namespace Tpf.Grpc.Server.Attributes
{
    /// <summary>
    /// GrpcServiceAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class GrpcServiceAttribute : Attribute
    {

    }
}
