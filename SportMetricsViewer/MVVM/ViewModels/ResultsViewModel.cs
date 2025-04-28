using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Domain.Abstractions.Dtos;
using SportMetricsViewer.Entities;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ResultsViewModel : ObservableObject
{
    private readonly IScoreCalculationService _scoreCalculationService;
    private readonly IExerciseService _exerciseService;

    private IReadOnlyList<ExerciseDto> Exercises { get; set; } = [];

    [ObservableProperty]
    private ExerciseType _selectedExerciseType;

    [ObservableProperty]
    private ExerciseDto? _selectedExercise;
    
    [ObservableProperty]
    private IList<ExerciseDto> _availableExercises = [];
    
    [ObservableProperty]
    private decimal _result;

    public static string NavigationRoute => "ResultsPage";

    public IReadOnlyList<ExerciseType> AvailableExerciseTypes { get; private set; } = Enum.GetValues<ExerciseType>();
    
    public ObservableCollection<ExerciseResult> ExerciseResults { get; } = [];

    public ResultsViewModel(IScoreCalculationService scoreCalculationService, IExerciseService exerciseService)
    {
        _scoreCalculationService = scoreCalculationService;
        _exerciseService = exerciseService;
        
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
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        Exercises = (await _exerciseService.GetExercisesAsync(cancellationToken)).ToList();
        UpdateAvailableExercisesCollection();
    }

    private void UpdateAvailableExercisesCollection()
    {
        AvailableExercises = GetAvailableExercises();
        SelectedExercise = AvailableExercises.FirstOrDefault();
    }

    private List<ExerciseDto> GetAvailableExercises()
    {
        return Exercises
            .Where(e => !ExerciseResults.Select(r => r.ExerciseId).Contains(e.Id))
            .Where(e => e.ExerciseType == SelectedExerciseType)
            .ToList();
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SelectedExerciseType))
        {
            UpdateAvailableExercisesCollection();
        }
    }
}
