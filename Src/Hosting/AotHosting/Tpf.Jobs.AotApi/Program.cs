//using System.Text.Json.Serialization;
using Hangfire;
using Tpf.Jobs.AotApi.Jobs;
using Tpf.Jobs.Hangfire;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddTpfHangfire();


var app = builder.Build();

app.UseTpfHangfireMiddle();

#region Hangfire Jobs 

await new TpfJobs().Do();

#endregion

app.Run();


