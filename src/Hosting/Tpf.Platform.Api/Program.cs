using Tpf.Domain.BaseInfo.HttpApi.RefitClient;
using Tpf.Middlewares;
using Tpf.Middlewares.Options;
using Tpf.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.AddOptions();

builder.AddCommonServiceExtensions();

builder.Services.AddBaseInfoDomainRefitClient();

builder.AddRabbitMq();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCommonAppMiddlewares();

app.UseShowAllServicesMiddleware(builder.Services);

app.Run();
