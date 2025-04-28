using SportMetricsViewer.Domain.Abstractions.Dtos;

namespace SportMetricsViewer.Domain.Abstractions;

public interface IExerciseService
{
    Task<IEnumerable<ExerciseDto>> GetExercisesAsync(CancellationToken cancellationToken = default);
}
