using SportMetricsViewer.Entities;

namespace SportMetricsViewer.Domain.Abstractions;

public interface IExercisesRepository
{
    Task<Exercise> GetAll();
}
