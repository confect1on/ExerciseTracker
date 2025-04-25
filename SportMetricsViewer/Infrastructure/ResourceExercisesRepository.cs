using System.Text.Json;
using System.Text.Json.Serialization;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Entities;

namespace SportMetricsViewer.Infrastructure;

internal sealed class ResourceExercisesRepository : IExercisesRepository
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    public async Task<List<Exercise>> GetAll(CancellationToken cancellationToken = default)
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync("exercises.json");
        var exercises = await JsonSerializer.DeserializeAsync<List<Exercise>>(stream, _jsonSerializerOptions, cancellationToken)
            ?? [];
        return exercises;
    }
}
