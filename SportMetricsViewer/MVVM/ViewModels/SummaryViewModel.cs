using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ExerciseTracker.Domain.Entities;

namespace SportMetricsViewer.MVVM.ViewModels;

[QueryProperty(nameof(ExerciseResults), nameof(ExerciseResults))]
public partial class SummaryViewModel : ObservableObject
{
    public static string NavigationRoute => "SummaryPage";
    
    [ObservableProperty]
    private ObservableCollection<ExerciseResult> exerciseResults = [];

    public SummaryViewModel()
    {
        ExerciseResults.CollectionChanged += (_, _) => SummaryScore = ExerciseResults
            .Select(r => r.Result)
            .Sum();
        PropertyChanged += (_, _) => SummaryScore = ExerciseResults
            .Select(r => r.Result)
            .Sum();
    }
    
    [ObservableProperty]
    private int summaryScore;
}
