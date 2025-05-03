using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using ExerciseTracker.Domain.Abstractions.Dtos;

namespace ExerciseTracker.MVVM.ViewModels;

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
