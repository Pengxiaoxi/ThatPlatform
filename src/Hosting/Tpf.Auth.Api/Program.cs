using Tpf.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddCommonServiceExtensions();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCommonAppMiddlewares();

app.UseShowAllServicesMiddleware(builder.Services);

app.Run();
