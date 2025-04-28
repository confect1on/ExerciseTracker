using SportMetricsViewer.Domain.Abstractions.Dtos;
using SportMetricsViewer.Entities;

namespace SportMetricsViewer.Domain.Abstractions;

public interface IExerciseService
{
    Task<IEnumerable<ExerciseDto>> GetExercisesAsync(CancellationToken cancellationToken = default);
}
