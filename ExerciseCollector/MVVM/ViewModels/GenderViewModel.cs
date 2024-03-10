using CommunityToolkit.Mvvm.Input;
using ExerciseCollector.Entities.Enums;

namespace ExerciseCollector.MVVM.ViewModels;

public partial class GenderViewModel
{
    [RelayCommand]
    public async Task NavigateToExerciseCollectorPage(Gender gender)
    {
        await Shell.Current.GoToAsync("exerciseCollector", new Dictionary<string, object>
        {
            
        });
    }
}
