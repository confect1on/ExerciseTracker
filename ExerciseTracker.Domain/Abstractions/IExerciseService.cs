using ExerciseTracker.Domain.Abstractions.Dtos;
using ExerciseTracker.Domain.Entities.Enums;

namespace ExerciseTracker.Domain.Abstractions;

public interface IExerciseService
{
    Task<IEnumerable<ExerciseDto>> GetExercisesAsync(Gender gender, ExerciseEntrantType exerciseEntrantType, CancellationToken cancellationToken = default);
}
