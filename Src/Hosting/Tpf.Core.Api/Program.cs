using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tpf.BaseRepository;
using Tpf.Dapper;
using Tpf.Dapper.Repository;
using Tpf.Domain.AuthInfo.Domain;
using Tpf.Middlewares;
using Tpf.MongoDB.Respository;

#region 设置环境变量demo【必须在 CreateBuilder 前才能覆盖 launchSettings.json 中的设置项，否则只能修改已启动进程中的环境变量】
//Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
//Console.WriteLine("\n" + "ASPNETCORE_ENVIRONMENT: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + "\n");

#endregion


var builder = WebApplication.CreateBuilder(args);

builder.AddCommonServiceExtensions();

#region Add Need ORM
// Main ORM：EF Core
builder.Services.AddDbContext<BaseInfoDbContext>();

// ORM：Dapper
builder.Services.AddTpfDapper();

builder.Services.AddKeyedScoped(typeof(IBaseRepository<>), "Mongo", typeof(MongoDBRepository<>));
//builder.Services.AddKeyedScoped(typeof(IBaseRepository<>), "Dapper", typeof(DapperRepository<>));

#endregion


var app = builder.Build();

app.UseCommonAppMiddlewares();

app.Run();



