using ExerciseTracker.Domain.Abstractions;
using ExerciseTracker.Domain.Abstractions.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Infrastructure.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services) => services
        .AddSingleton<IExercisesRepository, ResourceExercisesRepository>();
}
