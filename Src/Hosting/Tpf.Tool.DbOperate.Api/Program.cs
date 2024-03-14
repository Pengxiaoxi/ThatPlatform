using Microsoft.AspNetCore.Builder;
using Tpf.Middlewares;

#region ���û�������demo�������� CreateBuilder ǰ���ܸ��� launchSettings.json �е����������ֻ���޸������������еĻ���������
//Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
//Console.WriteLine("\n" + "ASPNETCORE_ENVIRONMENT: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + "\n");

#endregion


var builder = WebApplication.CreateBuilder(args);

builder.AddCommonServiceExtensions();

#region Test Code: Add Need ORM By NetCore Dependence Injection
// Main ORM��EF Core
//builder.Services.AddDbContext<AuthInfoDbContext>();

// ORM��Dapper
//builder.Services.AddTpfDapper();

//builder.Services.AddKeyedScoped(typeof(IBaseRepository<>), "Mongo", typeof(MongoDBRepository<>));
//builder.Services.AddKeyedScoped(typeof(IBaseRepository<>), "Dapper", typeof(Dapper<>));

#endregion


var app = builder.Build();

app.UseCommonAppMiddlewares();

app.UseShowAllServicesMiddleware(builder.Services);

app.Run();



