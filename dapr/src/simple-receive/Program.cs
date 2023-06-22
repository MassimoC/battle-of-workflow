
using Microsoft.EntityFrameworkCore;
using Dapr.Client;
using simple_model;

var DaprComponentName = "daprbus";
var TopicName = "inbound";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CivicStructuresDb>(opt => opt.UseInMemoryDatabase("CivicStructuresDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
var daprClient = new DaprClientBuilder().Build();

app.MapPost("/ParkingFacilities", async (ILogger<Program> logger, ParkingFacility parking, CivicStructuresDb civicStructuresDb) =>
{
    logger.LogInformation($"...... Receiver :: adding parking id to InMemory database - Id {parking.id}");
    civicStructuresDb.ParkingFacilities.Add(parking);

    logger.LogInformation($"...... Receiver :: publish ParkingFacility Id {parking.id} by component {DaprComponentName} via {TopicName}");
    await daprClient.PublishEventAsync<ParkingFacility>(DaprComponentName, TopicName, parking);

    logger.LogInformation($"...... Receiver :: save inmemory state");
    await civicStructuresDb.SaveChangesAsync();  

    return Results.Created($"/ParkingFacilities/{parking.id}", parking);
});

app.Run();
