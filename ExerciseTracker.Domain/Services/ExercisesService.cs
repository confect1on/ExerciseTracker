using ExerciseTracker.Domain.Abstractions;
using ExerciseTracker.Domain.Abstractions.DataAccess;
using ExerciseTracker.Domain.Abstractions.Dtos;
using ExerciseTracker.Domain.Entities.Enums;
using ExerciseTracker.Domain.Mappers;

namespace ExerciseTracker.Domain.Services;

internal sealed class ExercisesService(
    IExercisesRepository exercisesRepository) : IExerciseService
{
    public async Task<IEnumerable<ExerciseDto>> GetExercisesAsync(
        Gender gender,
        ExerciseEntrantType exerciseEntrantType,
        CancellationToken cancellationToken = default)
    {
        var exercises = await exercisesRepository.GetByFilters(
            gender,
            exerciseEntrantType,
            cancellationToken);
        
        return exercises.Select(e => e.MapToDto());
    }

    public async Task<ExerciseDto> GetExerciseByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var exercise = await exercisesRepository.GetById(id, cancellationToken);
        return exercise.MapToDto();
    }
}
