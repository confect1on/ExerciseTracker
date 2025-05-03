using ExerciseTracker.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Domain.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services) => services
        .AddSingleton<IExerciseService, ExercisesService>()
        .AddSingleton<IScoreCalculationService, ScoreCalculationService>()
        .AddSingleton<ISessionService, InMemorySessionService>();
}
