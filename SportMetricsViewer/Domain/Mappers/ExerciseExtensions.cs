using SportMetricsViewer.Domain.Abstractions.Dtos;
using SportMetricsViewer.Entities;

namespace SportMetricsViewer.Domain.Mappers;

public static class ExerciseExtensions
{
    public static ExerciseDto MapToDto(this Exercise exercise) => new(
        exercise.Id,
        exercise.Name,
        exercise.UnitOfMeasurementName,
        exercise.ExerciseType);
}
