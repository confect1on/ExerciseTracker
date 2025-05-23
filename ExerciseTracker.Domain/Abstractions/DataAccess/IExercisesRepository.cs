using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Entities.Enums;

namespace ExerciseTracker.Domain.Abstractions.DataAccess;

public interface IExercisesRepository
{
    Task<List<Exercise>> GetAll(CancellationToken cancellationToken = default);
    
    Task<List<Exercise>> GetByFilters(Gender gender, ExerciseEntrantType exerciseEntrantType, CancellationToken cancellationToken = default);
    
    Task<Exercise> GetById(int id, CancellationToken cancellationToken = default);
}
