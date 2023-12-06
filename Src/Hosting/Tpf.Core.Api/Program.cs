using Autofac;
using Microsoft.AspNetCore.Builder;
using Tpf.Autofac;
using Tpf.Middlewares;

#region ���û�������demo�������� CreateBuilder ǰ���ܸ��� launchSettings.json �е����������ֻ���޸������������еĻ���������
//Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
//Console.WriteLine("\n" + "ASPNETCORE_ENVIRONMENT: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + "\n");

#endregion


var builder = WebApplication.CreateBuilder(args);

builder.AddCommonServiceExtensions();

#region Add Need ORM
// Main ORM��EF Core
//builder.Services.AddDbContext<BaseInfoDbContext>();

// ORM��Dapper
//builder.Services.AddTpfDapper();

//builder.Services.AddKeyedScoped(typeof(IBaseRepository<>), "Mongo", typeof(MongoDBRepository<>));
//builder.Services.AddKeyedScoped(typeof(IBaseRepository<>), "Dapper", typeof(DapperRepository<>));

#endregion


var app = builder.Build();

app.UseCommonAppMiddlewares();

app.UseAllServicesMiddleware(builder.Services);

app.Run();



