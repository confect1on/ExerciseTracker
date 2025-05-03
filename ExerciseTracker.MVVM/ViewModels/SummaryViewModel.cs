using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.MVVM.ViewModels;

public partial class SummaryViewModel : ObservableObject
{
    public static string NavigationRoute => "SummaryPage";
    
    [ObservableProperty]
    private ObservableCollection<ExerciseResult> exerciseResults = [];

    public SummaryViewModel()
    {
        ExerciseResults.CollectionChanged += (_, _) => SummaryScore = Enumerable
            .Select<ExerciseResult, int>(ExerciseResults, r => r.Result)
            .Sum();
        PropertyChanged += (_, _) => SummaryScore = Enumerable
            .Select<ExerciseResult, int>(ExerciseResults, r => r.Result)
            .Sum();
    }
    
    [ObservableProperty]
    private int summaryScore;
}
