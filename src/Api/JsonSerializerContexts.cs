using System.Text.Json.Serialization;
using Domain.Entities;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    [JsonSerializable(typeof(PlayerDbDTO))]
    [JsonSerializable(typeof(PlayerDbDataDTO))]
    [JsonSerializable(typeof(CountryDbDTO))]
    [JsonSerializable(typeof(RootDbDTO))]
    internal partial class PlayerDbDTOsGenerationContext : JsonSerializerContext
    {
    }
    [JsonSerializable(typeof(IEnumerable<Player>))]
    [JsonSerializable(typeof(Player))]
    [JsonSerializable(typeof(PlayerData))]
    [JsonSerializable(typeof(Country))]

    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
    internal partial class PlayerGenerationContext : JsonSerializerContext
    {
    }

    [JsonSerializable(typeof(Dictionary<string, object>))]
    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Serialization)]
    internal partial class PlayersDirectoryGenerationContext : JsonSerializerContext
    {
    }
    [JsonSerializable(typeof(ProblemDetails))]
    [JsonSerializable(typeof(DateTime))]
    [JsonSourceGenerationOptions(WriteIndented = true,DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    internal partial class ProblemDetailsGenerationContext : JsonSerializerContext
    {
    }
}
