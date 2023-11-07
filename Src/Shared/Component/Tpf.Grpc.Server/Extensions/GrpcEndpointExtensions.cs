using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tpf.Grpc.Server.Attributes;

namespace Tpf.Grpc.Server
{
    /// <summary>
    /// GrpcEndpointExtensions
    /// </summary>
    public static class GrpcEndpointExtensions
    {
        /// <summary>
        /// MapGrpcServiceOfTPF 注册所有标记GrpcServiceAttribute的Grpc服务
        /// </summary>
        /// <param name="builder"></param>
        public static void MapGrpcServiceOfTPF(this IEndpointRouteBuilder builder)
        {
            Assembly[] assemblies = new Assembly[] { };
            assemblies = assemblies.Append(Assembly.Load("Tpf.BaseInfo.GrpcApplciation")).ToArray();
            List<Type> _modules = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract)
                .ToList();

            foreach (var type in _modules)
            {
                if (type.GetInterfaces().Length > 0 && type.GetInterfaces()[0].CustomAttributes.Any(x => x.AttributeType == typeof(GrpcServiceAttribute)))
                {
                    var method = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService").MakeGenericMethod(type);
                    method.Invoke(null, new[] { builder });
                }
            }

            // Default
            //builder.MapGrpcService<OrganizationGrpcService>();
        }
    }
}
