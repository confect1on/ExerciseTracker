namespace ExerciseTracker.Domain.Abstractions;

public interface IScoreCalculationService
{
    Task<int> CalculateScoreByResultAsync(
        int exerciseId,
        decimal resultInMeasurableUnits,
        CancellationToken cancellationToken);
}
