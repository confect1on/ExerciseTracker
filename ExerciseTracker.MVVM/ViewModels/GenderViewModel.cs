using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExerciseTracker.Domain.Entities.Enums;
using ExerciseTracker.MVVM.Abstractions;

namespace ExerciseTracker.MVVM.ViewModels;

public partial class GenderViewModel(SettingsViewModel settingsViewModel, INavigationService navigationService) : ObservableObject
{
    public static string NavigationRoute => "GenderPage";

    [RelayCommand]
    private async Task NavigateToExerciseCollectorPage(Gender gender)
    {
        settingsViewModel.Gender = gender;
        await navigationService.NavigateToAsync(SaveSessionViewModel.NavigationRoute);
    }
}
