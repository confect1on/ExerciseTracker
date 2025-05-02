using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SportMetricsViewer.Domain.Abstractions.Dtos;
using SportMetricsViewer.Extensions;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ExercisePickerViewModel : ObservableObject
{
    [ObservableProperty]
    private ExerciseDto? _selectedExercise;

    [ObservableProperty]
    private int _exercisesCount;

    public ExtendedObservableCollection<ExerciseDto> DisplayedExercises { get; } = [];
    
    public ExercisePickerViewModel()
    {
        DisplayedExercises.CollectionChanged += OnDisplayedExercisesOnCollectionChanged;
    }

    private void OnDisplayedExercisesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ExercisesCount = DisplayedExercises.Count;
    }
}
