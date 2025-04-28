using System.Text.Json;
using System.Text.Json.Serialization;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Entities;
using SportMetricsViewer.Entities.Enums;

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

    public async Task<List<Exercise>> GetByFilters(Gender gender, ExerciseEntrantType exerciseEntrantType, CancellationToken cancellationToken = default)
    {
        var exercises = await GetAll(cancellationToken);
        return exercises
            .Where(e => e.Gender == gender)
            .Where(e => e.ExerciseEntrantType == exerciseEntrantType)
            .ToList();
    }

    public async Task<Exercise> GetById(int id, CancellationToken cancellationToken = default)
    {
        var exercises = await GetAll(cancellationToken);
        return exercises.Single(e => e.Id == id);
    }
}
