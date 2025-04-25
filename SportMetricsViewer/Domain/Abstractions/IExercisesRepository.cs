using SportMetricsViewer.Entities;

namespace SportMetricsViewer.Domain.Abstractions;

public interface IExercisesRepository
{
    Task<List<Exercise>> GetAll(CancellationToken cancellationToken = default);
}
