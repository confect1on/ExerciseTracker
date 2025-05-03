using System.Text.Json;
using System.Text.Json.Serialization;
using ExerciseTracker.Domain.Abstractions;
using ExerciseTracker.Domain.Abstractions.DataAccess;
using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Entities.Enums;
using Microsoft.Maui.Storage;

namespace ExerciseTracker.Infrastructure.DataAccess;

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
        // TODO: remove dependency to MAUI package
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
