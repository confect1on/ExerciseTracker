using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportMetricsViewer.Entities;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.MVVM.ViewModels;

[QueryProperty(nameof(ExercisePickerModel), nameof(ExercisePickerModel))]
public partial class ResultsViewModel : ObservableObject
{
    private static readonly Dictionary<string, ExerciseType> ExerciseTypeNameToType = new()
    {
        { "Сила", ExerciseType.Strength },
        { "Быстрота и ловкость", ExerciseType.Agility },
        { "Выносливость", ExerciseType.Endurance }
    };
    
    public IList<string> ExerciseTypeNames { get; } = ExerciseTypeNameToType.Keys.ToList();

    public ObservableCollection<Exercise> ExercisesForChosenType { get; private set; } = [];
    
    [ObservableProperty]
    private string selectedExerciseTypeName;

    [ObservableProperty]
    private Exercise? selectedExerciseForChosenType;

    public ResultsViewModel()
    {
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

    private void RepopulateChosenExercisesCollection()
    {
        if (ExercisePickerModel is null)
        {
            throw new InvalidOperationException($"{nameof(ExercisePickerModel)} property is null");
        }
        ExercisesForChosenType.Clear();
        foreach (var exercise in Exercises
                     .Where(e => e.ExerciseType == ExerciseTypeNameToType[selectedExerciseTypeName])
                     .Where(e => e.Gender == ExercisePickerModel.Gender)
                     .Where(e => e.ExerciseEntrantType == ExercisePickerModel.ExerciseEntrantType)
                     .Where(e => !ExerciseResults.Select(r => r.Exercise).Contains(e))
                     .ToList())
        {
            ExercisesForChosenType.Add(exercise);
        }
        SelectedExerciseForChosenType = ExercisesForChosenType.FirstOrDefault();
    }

    [ObservableProperty]
    private int _selectedIndex;

    private bool _initialized;

    public IReadOnlyList<Exercise> Exercises { get; private set; } = [];
    
    public ExercisePickerModel? ExercisePickerModel { get; set; }

    [ObservableProperty]
    private decimal _result;
    
    public ObservableCollection<ExerciseResult> ExerciseResults { get; set; } = new();

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

        if (ExercisePickerModel is null)
        {
            throw new InvalidOperationException($"{nameof(ExercisePickerModel)} property is null");
        }

        if (ExercisePickerModel.ExerciseEntrantType is null)
        {
            throw new InvalidOperationException($"{nameof(ExercisePickerModel.ExerciseEntrantType)} property is null");
        }

        if (ExercisePickerModel.Gender is null)
        {
            throw new InvalidOperationException($"{nameof(ExercisePickerModel.Gender)} property is null");
        }
        
        var exerciseResult = new ExerciseResult
        {
            Exercise = SelectedExerciseForChosenType
                       ?? throw new InvalidOperationException($"{nameof(SelectedExerciseForChosenType)} is null"),
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

    [RelayCommand]
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (_initialized)
        {
            return;
        }

        Exercises = await ReadExercisesAsync(cancellationToken);
        ResetFields();
        _initialized = true;
    }

    private void ResetFields()
    {
        SelectedExerciseTypeName = ExerciseTypeNames.First();
        Result = 0;
    }

    private async Task<List<Exercise>> ReadExercisesAsync(CancellationToken cancellationToken = default)
    {
        var opt = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        await using var stream = await FileSystem.OpenAppPackageFileAsync("exercises.json");
        var exercises = await JsonSerializer.DeserializeAsync<List<Exercise>>(stream, opt, cancellationToken)
            ?? [];
        return exercises;
    }
}
