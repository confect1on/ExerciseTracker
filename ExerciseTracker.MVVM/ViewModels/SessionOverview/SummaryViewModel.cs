using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExerciseTracker.Domain.Abstractions;
using Microsoft.Maui.Controls;

namespace ExerciseTracker.MVVM.ViewModels.SessionOverview;

public partial class SummaryViewModel : ObservableObject, IQueryAttributable
{
    public static string NavigationRoute => "SummaryPage";

    private readonly ISessionService _sessionService;
    private readonly IScoreCalculationService _scoreCalculationService;
    private readonly IExerciseService _exerciseService;
    
    public ObservableCollection<ExerciseRecordDto> ExerciseRecordDtos { get; } = [];
    
    [ObservableProperty]
    private int _summaryScore;
    
    private Guid _sessionId;

    public SummaryViewModel(
        ISessionService sessionService,
        IScoreCalculationService scoreCalculationService,
        IExerciseService exerciseService)
    {
        _sessionService = sessionService;
        _scoreCalculationService = scoreCalculationService;
        _exerciseService = exerciseService;
        ExerciseRecordDtos.CollectionChanged += OnExerciseRecordDtosChanged;
    }

    private void OnExerciseRecordDtosChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        SummaryScore = ExerciseRecordDtos
            .Sum(x => x.Score);
    }

    [RelayCommand]
    private async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        var exerciseRecords = _sessionService.LoadSession(_sessionId);
        foreach (var exerciseRecord in exerciseRecords)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(exerciseRecord.ExerciseId, cancellationToken);
            var score = await _scoreCalculationService.CalculateScoreByResultAsync(
                exerciseRecord.ExerciseId,
                exerciseRecord.ResultInMeasurableUnit,
                cancellationToken);
            ExerciseRecordDtos.Add(new ExerciseRecordDto(exercise.Name, score));
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _sessionId = query["sessionId"] as Guid? ?? throw new InvalidOperationException("Cannot apply a query attribute without a SessionId");
    }
}
