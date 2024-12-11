using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Api;
using Application.Handlers;
using Application.Ports;
using Domain.Ports;

using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    var serializerOptions = options.SerializerOptions;
    
        serializerOptions.DefaultBufferSize = 4096;
        serializerOptions.Encoder = null;
        serializerOptions.WriteIndented = true;
        serializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        serializerOptions.AllowTrailingCommas = true;
        serializerOptions.PropertyNameCaseInsensitive = true;
        serializerOptions.TypeInfoResolver = JsonTypeInfoResolver.Combine(PlayerGenerationContext.Default,
            PlayerDbDTOsGenerationContext.Default,
            PlayersDirectoryGenerationContext.Default,
            ProblemDetailsGenerationContext.Default);
});

builder.Services.AddSingleton<IPlayersRepository, PlayersRepository>(p =>
{
    var serializerOptions = p.GetRequiredService<IOptions<Microsoft.AspNetCore.Http.Json.JsonOptions>> ().Value.SerializerOptions;
    const string fileDataFilePath = "headtohead.json";

    return new PlayersRepository(fileDataFilePath, serializerOptions);
});
builder.Services.AddSingleton<IPlayersHandler, PlayersHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();

var app = builder.Build();


var players = app.MapGroup("/players");
players.MapGet("/", async (HttpRequest request, [FromServices] IPlayersHandler playersHandler) =>
{
    var content = await playersHandler.GetAllPlayersAsync(request.HttpContext.RequestAborted);
    return !content.Any() ? Results.NoContent() : Results.Ok(content);
});
players.MapGet("/{id}", async (HttpRequest request, [FromServices] IPlayersHandler playersHandler, [FromRoute] int id) =>
{
    var content = await playersHandler.GetPlayerByIdAsync(id, request.HttpContext.RequestAborted);
    return Results.Ok(content);
});

var stats = app.MapGroup("/statistics");
stats.MapGet("/", async(HttpRequest request, [FromServices] IPlayersHandler playersHandler) =>
{
    var aggregate = await playersHandler.GetPlayersDirectoryAsync(request.HttpContext.RequestAborted);
    var averageBMI = aggregate.GetAverageBodyMassIndex();
    var medianHeight = aggregate.GetMedianHeightForAllPlayers();
    var countryHighestWinningPoints = aggregate.GetCountryWithHighestWinningPoints();

    var data = new Dictionary<string, object>
    {
        { "AverageBodyMassIndex", averageBMI },
        { "MedianHeight", medianHeight },
        { "CountryWithHighestWinningPoints", countryHighestWinningPoints }
    };

    return Results.Ok(data);
});


app.UseExceptionHandler();
app.UseStatusCodePages();

app.Run();

