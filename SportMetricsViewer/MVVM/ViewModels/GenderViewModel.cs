using CommunityToolkit.Mvvm.Input;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class GenderViewModel(SettingsViewModel settingsViewModel)
{
    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public async Task NavigateToExerciseCollectorPage(Gender gender)
    {
        settingsViewModel.Gender = gender;
        await Shell.Current.GoToAsync("exerciseCollector");
    }
}
