//using Tpf.Domain.BaseInfo.HttpApi.RefitClient;
using Tpf.Middlewares;
using Tpf.Platform.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddCommonServiceExtensions();

builder.Services.AddBaseInfoDomainRefitClient();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCommonAppMiddlewares();

app.UseShowAllServicesMiddleware(builder.Services);

app.Run();
