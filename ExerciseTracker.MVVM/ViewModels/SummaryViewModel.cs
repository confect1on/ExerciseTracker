using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ExerciseTracker.Domain.Abstractions;
using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.MVVM.ViewModels;

public partial class SummaryViewModel : ObservableObject
{
    private readonly ISessionService _sessionService;
    public static string NavigationRoute => "SummaryPage";
    
    [ObservableProperty]
    private ObservableCollection<ExerciseRecord> exerciseResults = [];

    public SummaryViewModel(ISessionService sessionService)
    {
        _sessionService = sessionService;
        ExerciseResults.CollectionChanged += (_, _) => SummaryScore = Enumerable
            .Select<ExerciseRecord, int>(ExerciseResults, r => r.Result)
            .Sum();
        PropertyChanged += (_, _) => SummaryScore = Enumerable
            .Select<ExerciseRecord, int>(ExerciseResults, r => r.Result)
            .Sum();
    }
    
    [ObservableProperty]
    private int summaryScore;
}
