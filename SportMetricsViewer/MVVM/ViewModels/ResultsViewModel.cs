using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Entities;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ResultsViewModel : ObservableObject
{
    private readonly SettingsViewModel _settingsViewModel;
    private readonly IExercisesRepository _exercisesRepository;

    private bool _initialized;

    [ObservableProperty]
    private ExerciseType _selectedExerciseType;

    [ObservableProperty]
    private Exercise? _selectedExerciseForChosenType;
    
    [ObservableProperty]
    private int _selectedIndex;
    
    [ObservableProperty]
    private decimal _result;

    public ObservableCollection<ExerciseType> ExerciseTypes { get; private set; } = new(Enum.GetValues<ExerciseType>());
    
    public IReadOnlyList<Exercise> Exercises { get; private set; } = [];
    
    public ObservableCollection<ExerciseResult> ExerciseResults { get; set; } = [];

    public ObservableCollection<Exercise> ExercisesForChosenType { get; private set; } = [];

    public static string NavigationRoute => "ResultsPage";

    public ResultsViewModel(
        SettingsViewModel settingsViewModel,
        IExercisesRepository exercisesRepository)
    {
        _settingsViewModel = settingsViewModel;
        _exercisesRepository = exercisesRepository;
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(SelectedExerciseType))
            {
                RepopulateChosenExercisesCollection();
            }
        };
        ExerciseResults.CollectionChanged += (_, _) =>
        {
            RepopulateChosenExercisesCollection();
        };
    }
    
    [RelayCommand]
    public async Task SaveResults()
    {
        if (ExerciseResults.Count == 3)
        {
            await Shell.Current.GoToAsync(SummaryViewModel.NavigationRoute, new Dictionary<string, object>
            {
                { nameof(ExerciseResults), ExerciseResults }
            });
        }
        
        var exerciseResult = new ExerciseResult
        {
            Exercise = SelectedExerciseForChosenType ?? throw new InvalidOperationException($"{nameof(SelectedExerciseForChosenType)} is null"),
            Result = CalculateResult()
        };
        ExerciseResults.Add(exerciseResult);
        ResetFields();
        if (ExerciseResults.Count == 3)
        {
            await Shell.Current.GoToAsync(
                SummaryViewModel.NavigationRoute,
                new Dictionary<string, object>
                {
                    { nameof(ExerciseResults), ExerciseResults }
                });
        }
    }

    [RelayCommand]
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (_initialized)
        {
            return;
        }

        Exercises = await _exercisesRepository.GetAll(cancellationToken);
        ResetFields();
        _initialized = true;
    }
    
    private int CalculateResult()
    {
        if (SelectedExerciseForChosenType is null)
        {
            throw new InvalidOperationException($"{nameof(SelectedExerciseForChosenType)} property is null");
        }

        var maxResult = SelectedExerciseForChosenType.Results.Keys.Max();
        var minResult = SelectedExerciseForChosenType.Results.Keys.Min();
        if (Result > maxResult)
        {
            return SelectedExerciseForChosenType.Results[maxResult];
        }

        if (Result < minResult)
        {
            return SelectedExerciseForChosenType.Results[minResult];
        }
        var calculatedResult = SelectedExerciseForChosenType.Results
            .FirstOrDefault(x => x.Key >= Result).Value;
        return calculatedResult;
    }

    private void ResetFields()
    {
        SelectedExerciseType = ExerciseTypes.First();
        Result = 0;
    }
    
    private void RepopulateChosenExercisesCollection()
    {
        ExercisesForChosenType.Clear();
        foreach (var exercise in Exercises
                     .Where(e => e.ExerciseType == SelectedExerciseType)
                     .Where(e => e.Gender == _settingsViewModel.Gender)
                     .Where(e => e.ExerciseEntrantType == _settingsViewModel.ExerciseEntrantType)
                     .Where(e => !ExerciseResults.Select(r => r.Exercise).Contains(e))
                     .ToList())
        {
            ExercisesForChosenType.Add(exercise);
        }
        SelectedExerciseForChosenType = ExercisesForChosenType.FirstOrDefault();
    }
}
