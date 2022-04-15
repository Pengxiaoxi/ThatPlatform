using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using ThatPlatform.Grpc.Server.Impl;

namespace ThatPlatform.Grpc.Server
{
    /// <summary>
    /// GrpcEndpointExtensions
    /// </summary>
    public static class GrpcEndpointExtensions
    {
        /// <summary>
        /// MapGrpcServiceOfTPF
        /// </summary>
        /// <param name="builder"></param>
        public static void MapGrpcServiceOfTPF(this IEndpointRouteBuilder builder)
        {
            // TODO: 后续可通过反射获取 继承 IGrpcServiceBase 的所有Grpc服务接口批量进行 MapGrpcService

            builder.MapGrpcService<OrganizationService>();
        }
    }
}
