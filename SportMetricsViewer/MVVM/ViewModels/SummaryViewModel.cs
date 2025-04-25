using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SportMetricsViewer.Entities;

namespace SportMetricsViewer.MVVM.ViewModels;

[QueryProperty(nameof(ExerciseResults), nameof(ExerciseResults))]
public partial class SummaryViewModel : ObservableObject
{
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
