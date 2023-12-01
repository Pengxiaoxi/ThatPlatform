using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;
using Tpf.Domain.AuthInfo.Domain;
using Tpf.Grpc.Server.Extensions;
using Tpf.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddCommonServiceExtensions();

#region Add Need ORM
// Main ORM：EF Core
//builder.Services.AddDbContext<BaseInfoDbContext>();

// ORM：Dapper
//builder.Services.AddTpfDapper();

#region Add gRpc Server
builder.Services.AddGrpc();

// 注册启用了代码优先的Grpc服务
builder.Services.AddCodeFirstGrpc();

// 注册启用反射的服务
builder.Services.AddGrpcReflectionOfTPF();
#endregion

#endregion


var app = builder.Build();

app.UseCommonAppMiddlewares();

app.Run();



