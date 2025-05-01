using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SportMetricsViewer.Entities.Enums;
using SportMetricsViewer.Extensions;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ExerciseTypePickerViewModel : ObservableObject
{
    [ObservableProperty]
    private ExerciseType _selectedExerciseType;

    public ExtendedObservableCollection<ExerciseType> ExerciseTypes { get; } = new(Enum.GetValues<ExerciseType>());

    public ExerciseTypePickerViewModel()
    {
        ExerciseTypes.CollectionChanged += ExerciseTypesOnCollectionChanged;
    }

    private void ExerciseTypesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        SelectedExerciseType = ExerciseTypes[0];
    }
}
