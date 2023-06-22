var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();

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