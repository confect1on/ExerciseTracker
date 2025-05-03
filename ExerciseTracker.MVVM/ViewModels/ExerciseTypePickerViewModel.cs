using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using ExerciseTracker.Domain.Entities.Enums;

namespace ExerciseTracker.MVVM.ViewModels;

public partial class ExerciseTypePickerViewModel : ObservableObject
{
    [ObservableProperty]
    private ExerciseType _selectedExerciseType;

    [ObservableProperty]
    private int _exerciseTypesCount;
    public ExtendedObservableCollection<ExerciseType> ExerciseTypes { get; } = new(Enum.GetValues<ExerciseType>());
    
    public ExerciseTypePickerViewModel()
    {
        ExerciseTypes.CollectionChanged += ExerciseTypesOnCollectionChanged;
    }

    private void ExerciseTypesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ExerciseTypesCount = ExerciseTypes.Count;
    }
}
