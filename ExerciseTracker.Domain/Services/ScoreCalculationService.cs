using ExerciseTracker.Domain.Abstractions;
using ExerciseTracker.Domain.Abstractions.DataAccess;

namespace ExerciseTracker.Domain.Services;

internal sealed class ScoreCalculationService(
    IExercisesRepository exercisesRepository) : IScoreCalculationService
{
    public async Task<int> CalculateScoreByResultAsync(
        int exerciseId,
        decimal resultInMeasurableUnits,
        CancellationToken cancellationToken)
    {
        var exercise = await exercisesRepository.GetById(exerciseId, cancellationToken);
        var maxResult = exercise.Results.Keys.Max();
        var minResult = exercise.Results.Keys.Min();
        if (resultInMeasurableUnits > maxResult)
        {
            return exercise.Results[maxResult];
        }

        if (resultInMeasurableUnits < minResult)
        {
            return exercise.Results[minResult];
        }
        var calculatedResult = exercise.Results
            .FirstOrDefault(x => x.Key >= resultInMeasurableUnits).Value;
        return calculatedResult;
    }
}
