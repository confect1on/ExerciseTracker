using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExerciseTracker.Domain.Entities.Enums;
using ExerciseTracker.MVVM.Abstractions;

namespace ExerciseTracker.MVVM.ViewModels;

public partial class ExerciseEntrantTypeViewModel(SettingsViewModel settingsViewModel, INavigationService navigationService) : ObservableObject
{
    public static string NavigationRoute => "ExerciseEntrantTypePage";

    [RelayCommand]
    private async Task NavigateToGenderPage(ExerciseEntrantType exerciseEntrantType)
    {
        settingsViewModel.ExerciseEntrantType = exerciseEntrantType;
        await navigationService.NavigateToAsync(GenderViewModel.NavigationRoute);
    }
}
