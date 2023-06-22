using simple_workflow;
using Dapr.Workflow;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<Orchestration>();
    options.RegisterActivity<DeclareToAuthorityActivity>();
    options.RegisterActivity<ArchiveActivity>();
});

// Dapr uses a random port for gRPC by default. If we don't know what that port
// is (because this app was started separate from dapr), then assume 50001.
if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DAPR_GRPC_PORT")))
{
    Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "50001");
}

var app = builder.Build();
app.UseRouting();
app.UseCloudEvents();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
    endpoints.MapControllers();
});
app.Run();