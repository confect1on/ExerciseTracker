using CommunityToolkit.Mvvm.Input;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ExerciseEntrantTypeViewModel(SettingsViewModel settingsViewModel)
{
    [RelayCommand]
    public async Task NavigateToGenderPage(ExerciseEntrantType exerciseEntrantType)
    {
        settingsViewModel.ExerciseEntrantType = exerciseEntrantType;
        await Shell.Current.GoToAsync("gender");
    }
}
