using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using ThatPlatform.Grpc.Server.Impl;

namespace ThatPlatform.Grpc.Server
{
    public static class GrpcEndpointExtension
    {
        public static void MapGrpcServiceOfTPF(this IEndpointRouteBuilder builder)
        {
            builder.MapGrpcService<OrganizationService>();
        }
    }
}
