using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExerciseTracker.Domain.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class GenderViewModel(SettingsViewModel settingsViewModel) : ObservableObject
{
    public static string NavigationRoute => "GenderPage";

    [RelayCommand]
    public async Task NavigateToExerciseCollectorPage(Gender gender)
    {
        settingsViewModel.Gender = gender;
        await Shell.Current.GoToAsync(SaveSessionViewModel.NavigationRoute);
    }
}
