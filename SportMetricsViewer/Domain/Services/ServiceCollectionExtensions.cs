using SportMetricsViewer.Domain.Abstractions;

namespace SportMetricsViewer.Domain.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services) => services
        .AddSingleton<IExerciseService, ExercisesService>()
        .AddSingleton<IScoreCalculationService, ScoreCalculationService>();
}
