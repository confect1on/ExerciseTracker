using CommunityToolkit.Mvvm.ComponentModel;
using ExerciseTracker.Domain.Entities.Enums;

namespace ExerciseTracker.MVVM.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private Gender _gender;

    [ObservableProperty]
    private ExerciseEntrantType _exerciseEntrantType;
}
