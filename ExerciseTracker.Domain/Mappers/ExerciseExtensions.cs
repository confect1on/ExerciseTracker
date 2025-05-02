using ExerciseTracker.Domain.Abstractions.Dtos;
using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.Domain.Mappers;

public static class ExerciseExtensions
{
    public static ExerciseDto MapToDto(this Exercise exercise) => new(
        exercise.Id,
        exercise.Name,
        exercise.UnitOfMeasurementName,
        exercise.ExerciseType);
}
