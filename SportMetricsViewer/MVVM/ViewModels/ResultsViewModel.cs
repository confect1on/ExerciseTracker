using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Entities;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

public partial class ResultsViewModel : ObservableObject
{
    private bool _initialized;
    private readonly SettingsViewModel _settingsViewModel;
    private readonly IExercisesRepository _exercisesRepository;

    [ObservableProperty]
    private string _selectedExerciseTypeName;

    [ObservableProperty]
    private Exercise? _selectedExerciseForChosenType;
    
    [ObservableProperty]
    private int _selectedIndex;
    
    [ObservableProperty]
    private decimal _result;

    private readonly static Dictionary<string, ExerciseType> ExerciseTypeNameToType = new()
    {
        { "Сила", ExerciseType.Strength },
        { "Быстрота и ловкость", ExerciseType.Agility },
        { "Выносливость", ExerciseType.Endurance }
    };
    
    public IReadOnlyList<Exercise> Exercises { get; private set; } = [];
    
    public ObservableCollection<ExerciseResult> ExerciseResults { get; set; } = [];
    
    public IList<string> ExerciseTypeNames { get; } = ExerciseTypeNameToType.Keys.ToList();

    public ObservableCollection<Exercise> ExercisesForChosenType { get; private set; } = [];

    public static string NavigationRoute => "SummaryPage";

    public ResultsViewModel(
        SettingsViewModel settingsViewModel,
        IExercisesRepository exercisesRepository)
    {
        _settingsViewModel = settingsViewModel;
        _exercisesRepository = exercisesRepository;
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(SelectedExerciseTypeName))
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
            await Shell.Current.GoToAsync("summary", new Dictionary<string, object>
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
                "summary",
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
        SelectedExerciseTypeName = ExerciseTypeNames.First();
        Result = 0;
    }
    
    private void RepopulateChosenExercisesCollection()
    {
        ExercisesForChosenType.Clear();
        foreach (var exercise in Exercises
                     .Where(e => e.ExerciseType == ExerciseTypeNameToType[SelectedExerciseTypeName])
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
