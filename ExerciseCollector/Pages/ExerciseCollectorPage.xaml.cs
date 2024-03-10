using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using ExerciseCollector.Entities;
using ExerciseCollector.Entities.Enums;

namespace ExerciseCollector.Pages;

public partial class ExerciseCollectorPage : ContentPage
{
    private readonly Dictionary<string, ExerciseType> nameToType = new()
    {
        { "Сила", ExerciseType.Strength },
        { "Быстрота и ловкость", ExerciseType.Agility },
        { "Выносливость", ExerciseType.Endurance }
    };
    private string selectedExerciseTypeName;
    private string? selectedExerciseName;
    public IList<string> ExerciseTypes { get; }

    public string SelectedExerciseTypeName
    {
        get => selectedExerciseTypeName;
        set
        {
            selectedExerciseTypeName = value;
            ExerciseNames.Clear();
            foreach (var exerciseName in Exercises
                         .Where(e => e.ExerciseType == nameToType[selectedExerciseTypeName])
                         .Select(e => $"{e.Name} ({e.UnitOfMeasurementName})")
                         .ToList())
            {
                ExerciseNames.Add(exerciseName);
            }
        }
    }

    public IReadOnlyList<Exercise> Exercises { get; } = new List<Exercise>
    {
        new()
        {
            Gender = Gender.Male,
            ExerciseType = ExerciseType.Strength,
            Name = "Сгибание и разгибание рук в упоре лежа",
            UnitOfMeasurementName = "Кол-во раз",
            Results = new SortedList<decimal, int>
            {
                {0, 0},
                {1, 1},
                {2, 5},
                {3, 10},
                {4, 13},
                {5, 15},
                {6, 17},
                {7, 19},
                {8, 20},
                {9, 23},
                {10, 25},
                {11, 26},
                {12, 27},
                {13, 28},
                {14, 29},
                {15, 30},
                {16, 31},
                {17, 32},
                {18, 33},
                {19, 34},
                {20, 35},
                {21, 36},
                {22, 37},
                {23, 38},
                {24, 39},
                {25, 40},
                {26, 41},
                {27, 42},
                {28, 43},
                {29, 44},
                {30, 45},
                {31, 46},
                {32, 47},
                {33, 48},
                {34, 49},
                {35, 50},
                {36, 51},
                {37, 52},
                {38, 53},
                {39, 54},
                {40, 55},
                {41, 57},
                {42, 59},
                {43, 61},
                {44, 63},
                {45, 65},
                {46, 67},
                {47, 69},
                {48, 71},
                {49, 73},
                {50, 75},
                {51, 77},
                {52, 79},
                {53, 81},
                {54, 83},
                {55, 85},
                {56, 87},
                {57, 89},
                {58, 91},
                {59, 93},
                {60, 95},
                {61, 97},
                {62, 99},
                {63, 100},
                {64, 100},
                {65, 100},
                {66, 100},
                {67, 100},
                {68, 100},
                {69, 100},
                {70, 100},
                {71, 100},
                {72, 100},
                {73, 100},
                {74, 100},
                {75, 100},
                {76, 100},
                {77, 100}
            }
        },
        new()
        {
            Gender = Gender.Male,
            ExerciseType = ExerciseType.Strength,
            Name = "Подтягивание на перекладине",
            UnitOfMeasurementName = "За 1 мин / Кол-во раз",
            Results = new SortedList<decimal, int>
            {
                {0, 0},
                {1, 1},
                {2, 23},
                {3, 28},
                {4, 33},
                {5, 38},
                {6, 43},
                {7, 48},
                {8, 50},
                {9, 53},
                {10, 58},
                {11, 61},
                {12, 63},
                {13, 65},
                {14, 67},
                {15, 69},
                {16, 71},
                {17, 73},
                {18, 75},
                {19, 77},
                {20, 79},
                {21, 81},
                {22, 83},
                {23, 85},
                {24, 87},
                {25, 89},
                {26, 91},
                {27, 93},
                {28, 95},
                {29, 97},
                {30, 99},
                {31, 100},
                {32, 100},
                {33, 100},
                {34, 100},
                {35, 100},
                {36, 100},
                {37, 100},
                {38, 100},
                {39, 100},
                {40, 100},
                {41, 100},
                {42, 100},
                {43, 100},
                {44, 100},
                {45, 100}
            }
        },
        new()
        {
            Gender = Gender.Male,
            ExerciseType = ExerciseType.Agility,
            Name = "Челночный бег 10x10 м",
            UnitOfMeasurementName = "Сек.",
            Results = new SortedList<decimal, int>
            {
                {0, 0},
                {10.0m, 100},
                {24.0m, 99},
                {24.1m, 98},
                {24.2m, 97},
                {24.3m, 96},
                {24.4m, 95},
                {24.5m, 94},
                {24.6m, 93},
                {24.7m, 92},
                {24.8m, 91},
                {24.9m, 90},
                {25.0m, 88},
                {25.1m, 86},
                {25.2m, 84},
                {25.3m, 82},
                {25.4m, 80},
                {25.5m, 78},
                {25.6m, 76},
                {25.7m, 74},
                {25.8m, 72},
                {25.9m, 70},
                {26.0m, 68},
                {26.1m, 66},
                {26.2m, 64},
                {26.3m, 62},
                {26.4m, 60},
                {26.5m, 58},
                {26.6m, 57},
                {26.7m, 56},
                {26.8m, 55},
                {26.9m, 54},
                {27.0m, 53},
                {27.1m, 52},
                {27.2m, 51},
                {27.3m, 50},
                {27.4m, 49},
                {27.8m, 48},
                {28.1m, 47},
                {28.3m, 46},
                {28.5m, 45},
                {28.6m, 44},
                {28.9m, 43},
                {30.1m, 42},
                {30.5m, 41},
                {30.9m, 40},
                {31.1m, 39},
                {31.7m, 38},
                {32.1m, 37},
                {32.8m, 36},
                {33.5m, 35},
                {34.1m, 34},
                {34.5m, 33},
                {34.9m, 32},
                {35.3m, 31},
                {35.7m, 30},
                {36.1m, 29},
                {36.6m, 28},
                {37.1m, 27},
                {37.8m, 26},
                {38.5m, 25},
                {39.1m, 24},
                {39.6m, 23},
                {40.1m, 22},
                {40.8m, 21},
                {41.5m, 20},
                {42.1m, 19},
                {42.2m, 18},
                {42.3m, 17},
                {42.4m, 16},
                {42.5m, 15},
                {42.6m, 14},
                {42.7m, 13},
                {42.8m, 12},
                {42.9m, 11},
                {43.0m, 10},
                {43.1m, 9},
                {43.2m, 8},
                {43.3m, 7},
                {43.4m, 6},
                {43.5m, 5},
                {43.6m, 4},
                {43.7m, 3},
                {43.8m, 2},
                {43.9m, 1},
                {44.0m, 0},
                {45.0m, 0},
                {46.0m, 0},
                {47.0m, 0},
                {48.0m, 0},
                {49.0m, 0},
                {50.0m, 0},
                {51.0m, 0},
                {52.0m, 0},
                {53.0m, 0},
                {54.0m, 0},
                {55.0m, 0},
                {56.0m, 0},
                {57.0m, 0}
            }
        }
    };

    public ObservableCollection<string> ExerciseNames { get; }

    public string? SelectedExerciseName
    {
        get => selectedExerciseName;
        set
        {
            if (value == selectedExerciseName || value == null && ExerciseNames.Count > 0) return;
            selectedExerciseName = value;
            OnPropertyChanged();
        }
    }

    public ExerciseCollectorPage()
    {
        ExerciseNames = new();
        ExerciseNames.CollectionChanged += (_, _) =>
        {
            SelectedExerciseName = ExerciseNames.FirstOrDefault();
        };
        ExerciseTypes = new List<string>
        {
            "Сила", "Быстрота и ловкость", "Выносливость"
        };
        SelectedExerciseTypeName = ExerciseTypes.First();
        InitializeComponent();
    }

    public ObservableCollection<ExerciseResult> Results { get; set; } = new();
    
    public decimal Result { get; set; }
    private async void Button_OnClicked(object? sender, EventArgs eventArgs)
    {

        var exercise = Exercises
            .Where(e => e.Gender == Gender.Male)
            .Where(e => e.ExerciseType == nameToType[selectedExerciseTypeName])
            .First(e => $"{e.Name} ({e.UnitOfMeasurementName})" == SelectedExerciseName);
        if (Results.FirstOrDefault(r => r.Exercise.Name == exercise.Name) is not null)
        {
            return;
        }
        var result = new ExerciseResult
        {
            Exercise = exercise,
            Result = exercise.Results.First(x => x.Key >= Result).Value
        };
        Results.Add(result);
        if (Results.Count == 3)
        {
            await Navigation.PushAsync(new ResultPage(Results));
        }
    }
}
