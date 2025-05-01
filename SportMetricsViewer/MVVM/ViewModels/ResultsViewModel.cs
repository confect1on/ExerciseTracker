using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json;
using AndroidX.ConstraintLayout.Motion.Widget;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Domain.Abstractions.Dtos;
using SportMetricsViewer.Entities;
using SportMetricsViewer.Entities.Enums;
using SportMetricsViewer.Extensions;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ResultsViewModel : ObservableObject
{
    private readonly IScoreCalculationService _scoreCalculationService;
    private readonly IExerciseService _exerciseService;
    private readonly ILogger<ResultsViewModel> _logger;

    public ExerciseTypePickerViewModel ExerciseTypePickerViewModel { get; } = new();
    
    public ExercisePickerViewModel ExercisePickerViewModel { get; } = new();

    [ObservableProperty]
    private ExtendedObservableCollection<ExerciseDto> _exercises = [];
    
    [ObservableProperty]
    private ExtendedObservableCollection<ExerciseDto> _availableExercises = [];
    
    [ObservableProperty]
    private decimal _result;

    public static string NavigationRoute => "ResultsPage";
    
    public ExtendedObservableCollection<ExerciseResult> ExerciseResults { get; } = [];

    public ResultsViewModel(
        IScoreCalculationService scoreCalculationService,
        IExerciseService exerciseService,
        ILogger<ResultsViewModel> logger)
    {
        _scoreCalculationService = scoreCalculationService;
        _exerciseService = exerciseService;
        _logger = logger;
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
    }

    private void RefillDisplayedExercises()
    {
        var newDisplayedExercises = AvailableExercises
            .Where(ex => ex.ExerciseType == ExerciseTypePickerViewModel.SelectedExerciseType)
            .ToArray();
        ExercisePickerViewModel.DisplayedExercises.RefillBy(newDisplayedExercises);
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
        // ArgumentNullException.ThrowIfNull(ExercisePickerViewModel.SelectedExercise);
        // var selectedExerciseId = ExercisePickerViewModel.SelectedExercise.Id;
        // var currentResult = new ExerciseResult
        // {
        //     ExerciseId = selectedExerciseId,
        //     Result = await _scoreCalculationService.CalculateScoreByResultAsync(selectedExerciseId, Result, cancellationToken)
        // };
        // ExerciseResults.Add(currentResult);
        // Result = 0;
    }
}
