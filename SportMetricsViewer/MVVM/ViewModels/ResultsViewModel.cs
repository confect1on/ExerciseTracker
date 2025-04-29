using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Domain.Abstractions.Dtos;
using SportMetricsViewer.Entities;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ResultsViewModel : ObservableObject
{
    private readonly IScoreCalculationService _scoreCalculationService;

    private readonly IExerciseService _exerciseService;
    private readonly ILogger<ResultsViewModel> _logger;

    [ObservableProperty]
    private IList<ExerciseDto> _exercises = [];
    
    [ObservableProperty]
    private IList<ExerciseDto> _availableExercises = [];

    [ObservableProperty]
    private IList<ExerciseType> _availableExerciseTypes = Enum.GetValues<ExerciseType>().ToList();

    [ObservableProperty]
    private ExerciseType _selectedExerciseType = ExerciseType.Strength;

    [ObservableProperty]
    private ExerciseDto? _selectedExercise;
    
    [ObservableProperty]
    private IList<ExerciseDto> _displayedExercises = [];
    
    [ObservableProperty]
    private decimal _result;

    public static string NavigationRoute => "ResultsPage";
    
    public ObservableCollection<ExerciseResult> ExerciseResults { get; } = [];

    public ResultsViewModel(
        IScoreCalculationService scoreCalculationService,
        IExerciseService exerciseService,
        ILogger<ResultsViewModel> logger)
    {
        _scoreCalculationService = scoreCalculationService;
        _exerciseService = exerciseService;
        _logger = logger;
        PropertyChanged += OnPropertyChanged;
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public async Task SaveResult(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(SelectedExercise);
        var currentResult = new ExerciseResult
        {
            ExerciseId = SelectedExercise.Id,
            Result = await _scoreCalculationService.CalculateScoreByResultAsync(SelectedExercise.Id, Result, cancellationToken)
        };
        ExerciseResults.Add(currentResult);
        Result = 0;
        if (ExerciseResults.Count == 3)
        {
            await Shell.Current.GoToAsync(SummaryViewModel.NavigationRoute);
        }
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        Exercises = (await _exerciseService.GetExercisesAsync(cancellationToken)).ToList();
    }

    private void UpdateAvailableExercises()
    {
        AvailableExercises = Exercises
            .Where(e => !ExerciseResults.Select(r => r.ExerciseId).Contains(e.Id))
            .ToList();
    }

    private void UpdateAvailableExerciseTypes()
    {
        AvailableExerciseTypes = AvailableExercises
            .Select(x => x.ExerciseType)
            .Distinct()
            .ToList();
    }

    private void UpdateSelectedExerciseType()
    {
        SelectedExerciseType = AvailableExerciseTypes.First();
        _logger.LogInformation("{ExerciseType} has selected", SelectedExerciseType);
    }

    private void UpdateDisplayedExercises()
    {
        DisplayedExercises = AvailableExercises
            .Where(e => e.ExerciseType == SelectedExerciseType)
            .ToList();
        _logger.LogInformation("Displayed exercises changed");
    }

    private void UpdateSelectedExercise()
    {
        SelectedExercise = DisplayedExercises.FirstOrDefault();
        _logger.LogInformation("Selected exercise updated. new exercise: {ExerciseId}", SelectedExercise?.Id);
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Exercises):
                _logger.LogInformation("Exercises changed");
                UpdateAvailableExercises();
                break;

            case nameof(SelectedExerciseType):
                _logger.LogInformation("Selected exercise type changed");
                UpdateDisplayedExercises();
                break;

            case nameof(AvailableExercises):
                _logger.LogInformation("Available exercises changed");
                UpdateAvailableExerciseTypes();
                UpdateDisplayedExercises();
                break;
            
            case nameof(AvailableExerciseTypes):
                _logger.LogInformation("Available exercise types changed");
                UpdateSelectedExerciseType();
                break;

            case nameof(ExerciseResults):
                _logger.LogInformation("Exercise results changed");
                UpdateAvailableExercises();
                break;
            
            case nameof(DisplayedExercises):
                using (_logger.BeginScope("Displayed exercises changed"));
                UpdateSelectedExercise();
                break;
        }
    }
}
