using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Domain.Abstractions.Dtos;
using SportMetricsViewer.Domain.Mappers;
using SportMetricsViewer.MVVM.ViewModels;

namespace SportMetricsViewer.Domain.Services;

internal sealed class ExercisesService(
    SettingsViewModel settingsViewModel,
    IExercisesRepository exercisesRepository) : IExerciseService
{
    public async Task<IEnumerable<ExerciseDto>> GetExercisesAsync(CancellationToken cancellationToken = default)
    {
        var exercises = await exercisesRepository.GetByFilters(
            settingsViewModel.Gender,
            settingsViewModel.ExerciseEntrantType,
            cancellationToken);
        
        return exercises.Select(e => e.MapToDto());
    }
}
