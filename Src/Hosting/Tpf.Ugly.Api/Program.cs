using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;
using Tpf.Domain.AuthInfo.Domain;
using Tpf.Grpc.Server.Extensions;
using Tpf.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddCommonServiceExtensions();

#region Add Need ORM
// Main ORM��EF Core
//builder.Services.AddDbContext<BaseInfoDbContext>();

// ORM��Dapper
//builder.Services.AddTpfDapper();

#region Add gRpc Server
builder.Services.AddGrpc();

// ע�������˴������ȵ�Grpc����
builder.Services.AddCodeFirstGrpc();

// ע�����÷���ķ���
builder.Services.AddGrpcReflectionOfTPF();
#endregion

#endregion


var app = builder.Build();

app.UseCommonAppMiddlewares();

app.Run();



