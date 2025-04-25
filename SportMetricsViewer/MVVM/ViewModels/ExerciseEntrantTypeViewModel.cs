using CommunityToolkit.Mvvm.Input;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ExerciseEntrantTypeViewModel
{
    public ExercisePickerModel ExercisePickerModel { get; } = new();

    [RelayCommand]
    public async Task NavigateToGenderPage(ExerciseEntrantType exerciseEntrantType)
    {
        if (ExercisePickerModel is null)
        {
            throw new InvalidOperationException($"{nameof(ExercisePickerModel)} property is null");
        }

        ExercisePickerModel.ExerciseEntrantType = exerciseEntrantType;
        await Shell.Current.GoToAsync("gender", new Dictionary<string, object>
        {
            {
                nameof(ExercisePickerModel),
                ExercisePickerModel
            } 
        });
    }
}
