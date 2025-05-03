using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExerciseTracker.Domain.Abstractions;
using ExerciseTracker.Domain.Abstractions.Dtos;
using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Entities.Enums;
using ExerciseTracker.MVVM.Abstractions;

namespace ExerciseTracker.MVVM.ViewModels;

public partial class SaveSessionViewModel : ObservableObject
{
    public static string NavigationRoute => "SaveSessionPage";

    private readonly IScoreCalculationService _scoreCalculationService;
    private readonly IExerciseService _exerciseService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private ExtendedObservableCollection<ExerciseDto> _exercises = [];
    
    [ObservableProperty]
    private ExtendedObservableCollection<ExerciseDto> _availableExercises = [];
    
    [ObservableProperty]
    private decimal _result;

    [ObservableProperty]
    private int _exerciseRecordsCount;

    public int MaxExerciseRecordsPerSession => 3;
    
    public ExerciseTypePickerViewModel ExerciseTypePickerViewModel { get; } = new();
    
    public ExercisePickerViewModel ExercisePickerViewModel { get; } = new();
    
    public ExtendedObservableCollection<ExerciseResult> ExerciseResults { get; } = [];

    public SaveSessionViewModel(
        IScoreCalculationService scoreCalculationService,
        IExerciseService exerciseService,
        INavigationService navigationService)
    {
        _scoreCalculationService = scoreCalculationService;
        _exerciseService = exerciseService;
        _navigationService = navigationService;
        AvailableExercises.CollectionChanged += OnAvailableExercisesChanged;
        ExerciseTypePickerViewModel.PropertyChanged += OnExerciseTypeViewModelChanged;
        ExerciseResults.CollectionChanged += ExerciseResultsOnCollectionChanged;
        AvailableExercises.AddRange(new List<ExerciseDto>(){
            new(1, "1", "s", ExerciseType.Strength),
            new(2, "2", "m", ExerciseType.Strength),
            new(3, "3", "m", ExerciseType.Agility),
        });
    }

    private void OnAvailableExercisesChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RefillDisplayedExercises();
        UpdateExerciseTypes();
    }

    private void UpdateExerciseTypes()
    {
        var availableExerciseTypes = AvailableExercises
            .Select(x => x.ExerciseType)
            .Distinct();
        var exerciseTypesToDelete =
            ExerciseTypePickerViewModel.ExerciseTypes
                .Except(availableExerciseTypes)
                .ToArray();
        foreach (var exerciseType in exerciseTypesToDelete)
        {
            ExerciseTypePickerViewModel.ExerciseTypes.Remove(exerciseType);
        }
        ExerciseTypePickerViewModel.SelectedExerciseType = ExerciseTypePickerViewModel.ExerciseTypes.First();
    }

    private void RefillDisplayedExercises()
    {
        var newDisplayedExercises = AvailableExercises
            .Where(ex => ex.ExerciseType == ExerciseTypePickerViewModel.SelectedExerciseType)
            .ToArray();
        ExercisePickerViewModel.DisplayedExercises.RefillBy(newDisplayedExercises);
        // TODO I literally don't know why this doesn't work when triggered by event. even in UI-thread 
        ExercisePickerViewModel.SelectedExercise = ExercisePickerViewModel.DisplayedExercises.FirstOrDefault();
    }

    private void ExerciseResultsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        var exerciseIdToDelete = e.NewItems?
            .Cast<ExerciseResult>()
            .Select(r => r.ExerciseId)
            .FirstOrDefault();
        if (exerciseIdToDelete == null)
        {
            return;
        }
        var exercisesToDelete = AvailableExercises
            .First(x => x.Id == exerciseIdToDelete);
        AvailableExercises.Remove(exercisesToDelete);
        ExerciseRecordsCount = ExerciseResults.Count;
    }

    private void OnExerciseTypeViewModelChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ExerciseTypePickerViewModel.SelectedExerciseType):
            {
                RefillDisplayedExercises();
                break;
            }
        }
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public async Task SaveResult(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(ExercisePickerViewModel.SelectedExercise);
        var selectedExerciseId = ExercisePickerViewModel.SelectedExercise.Id;
        var currentResult = new ExerciseResult
        {
            ExerciseId = selectedExerciseId,
            Result = await _scoreCalculationService.CalculateScoreByResultAsync(selectedExerciseId, Result, cancellationToken)
        };
        ExerciseResults.Add(currentResult);
        Result = 0;
        if (ExerciseResults.Count == MaxExerciseRecordsPerSession)
        {
            await _navigationService.NavigateToAsync(SummaryViewModel.NavigationRoute);
        }
    }
}
