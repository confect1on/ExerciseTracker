using ExerciseTracker.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Infrastructure.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services) => services
        .AddSingleton<IExercisesRepository, ResourceExercisesRepository>();
}
