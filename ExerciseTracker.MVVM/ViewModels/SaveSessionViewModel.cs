using System.Collections.ObjectModel;
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

    private readonly SettingsViewModel _settingsViewModel;
    private readonly IScoreCalculationService _scoreCalculationService;
    private readonly IExerciseService _exerciseService;
    private readonly ISessionService _sessionService;
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
    
    public ExtendedObservableCollection<ExerciseRecord> ExerciseRecords { get; } = [];

    public SaveSessionViewModel(
        SettingsViewModel settingsViewModel,
        IScoreCalculationService scoreCalculationService,
        IExerciseService exerciseService,
        ISessionService sessionService,
        INavigationService navigationService)
    {
        _settingsViewModel = settingsViewModel;
        _scoreCalculationService = scoreCalculationService;
        _exerciseService = exerciseService;
        _sessionService = sessionService;
        _navigationService = navigationService;

        AvailableExercises.CollectionChanged += OnAvailableExercisesChanged;
        ExerciseTypePickerViewModel.PropertyChanged += OnExerciseTypeViewModelChanged;
        ExerciseRecords.CollectionChanged += ExerciseRecordsOnCollectionChanged;
        Exercises.CollectionChanged += OnExercisesCollectionChanged;
    }

    [RelayCommand]
    private Task SaveResultAsync()
    {
        ArgumentNullException.ThrowIfNull(ExercisePickerViewModel.SelectedExercise);
        var selectedExerciseId = ExercisePickerViewModel.SelectedExercise.Id;
        return SaveResultInternal(selectedExerciseId);
    }

    [RelayCommand]
    private async Task InitializeExercisesAsync(CancellationToken cancellationToken = default)
    {
        var exercises = await _exerciseService.GetExercisesAsync(
            _settingsViewModel.Gender,
            _settingsViewModel.ExerciseEntrantType,
            cancellationToken);
        Exercises.AddRange(exercises.ToList());
    }

    private void OnExercisesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        var newExercises = e.NewItems?
            .Cast<ExerciseDto>()
            .ToList();
        if (newExercises != null)
        {
            AvailableExercises.AddRange(newExercises);
        }
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
        // TODO: better to use nullable type here, but it causes to rewrite part of the code
        ExerciseTypePickerViewModel.SelectedExerciseType = ExerciseTypePickerViewModel.ExerciseTypes[0];
    }

    private void RefillDisplayedExercises()
    {
        var newDisplayedExercises = AvailableExercises
            .Where(ex => ex.ExerciseType == ExerciseTypePickerViewModel.SelectedExerciseType)
            .ToArray();
        ExercisePickerViewModel.DisplayedExercises.RefillBy(newDisplayedExercises);
        // TODO: I literally don't know why this doesn't work when triggered by event. even in UI-thread 
        ExercisePickerViewModel.SelectedExercise = ExercisePickerViewModel.DisplayedExercises.FirstOrDefault();
    }

    private void ExerciseRecordsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        var exerciseIdToDelete = e.NewItems?
            .Cast<ExerciseRecord>()
            .Select(r => r.ExerciseId)
            .FirstOrDefault();
        if (exerciseIdToDelete == null)
        {
            return;
        }
        var exercisesToDelete = AvailableExercises
            .First(x => x.Id == exerciseIdToDelete);
        AvailableExercises.Remove(exercisesToDelete);
        ExerciseRecordsCount = ExerciseRecords.Count;
    }

    private void OnExerciseTypeViewModelChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ExerciseTypePickerViewModel.SelectedExerciseType))
        {
            RefillDisplayedExercises();
        }
    }

    private async Task SaveResultInternal(int selectedExerciseId)
    {
        var currentResult = new ExerciseRecord
        {
            ExerciseId = selectedExerciseId,
            ResultInMeasurableUnit = Result,
        };
        ExerciseRecords.Add(currentResult);
        Result = 0;
        if (ExerciseRecords.Count == MaxExerciseRecordsPerSession)
        {
            var sessionId = _sessionService.SaveSession(ExerciseRecords);
            await _navigationService.NavigateToAsync(SessionOverview.SummaryViewModel.NavigationRoute, new Dictionary<string, object>
            {
                {
                    "sessionId",
                    sessionId
                }
            });
        }
    }
}
