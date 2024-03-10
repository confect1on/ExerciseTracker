using CommunityToolkit.Mvvm.Input;
using ExerciseCollector.Entities.Enums;

namespace ExerciseCollector.MVVM.ViewModels;

public partial class ExerciseEntrantTypeViewModel
{
    [RelayCommand]
    public async Task NavigateToGenderPage(ExerciseEntrantType exerciseEntrantType)
    {
        await Shell.Current.GoToAsync("gender", new Dictionary<string, object>
        {
            {
                nameof(ExerciseEntrantType),
                exerciseEntrantType
            } 
        });
    }
}
