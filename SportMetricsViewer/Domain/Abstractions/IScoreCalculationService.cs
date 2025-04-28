namespace SportMetricsViewer.Domain.Abstractions;

public interface IScoreCalculationService
{
    public Task<int> CalculateScoreByResultAsync(int exerciseId, decimal resultInMeasurableUnits, CancellationToken cancellationToken);
}
