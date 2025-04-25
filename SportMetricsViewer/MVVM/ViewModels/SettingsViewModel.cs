using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private Gender _gender;

    [ObservableProperty]
    private ExerciseEntrantType _exerciseEntrantType;
}
