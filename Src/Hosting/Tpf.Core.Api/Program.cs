using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tpf.Dapper;
using Tpf.Domain.AuthInfo.Domain;
using Tpf.Middlewares;

#region ���û�������demo�������� CreateBuilder ǰ���ܸ��� launchSettings.json �е����������ֻ���޸������������еĻ���������
//Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
//Console.WriteLine("\n" + "ASPNETCORE_ENVIRONMENT: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + "\n");

#endregion


var builder = WebApplication.CreateBuilder(args);

builder.AddCommonServiceExtensions();

#region Add Need ORM
// Main ORM��EF Core
builder.Services.AddDbContext<BaseInfoDbContext>();

// ORM��Dapper
builder.Services.AddTpfDapper();

#endregion


var app = builder.Build();

app.UseCommonAppMiddlewares();

app.Run();



