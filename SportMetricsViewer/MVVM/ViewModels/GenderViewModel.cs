using CommunityToolkit.Mvvm.Input;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

[QueryProperty(nameof(ExercisePickerModel), nameof(ExercisePickerModel))]
public partial class GenderViewModel
{
    public ExercisePickerModel? ExercisePickerModel { get; set; }

    [RelayCommand]
    public async Task NavigateToExerciseCollectorPage(Gender gender)
    {
        if (ExercisePickerModel is null)
        {
            throw new InvalidOperationException($"{nameof(ExercisePickerModel)} property is null");
        }

        ExercisePickerModel.Gender = gender;
        await Shell.Current.GoToAsync("exerciseCollector", new Dictionary<string, object>
        {
            {
                nameof(ExercisePickerModel),
                ExercisePickerModel
            }
        });
    }
}
