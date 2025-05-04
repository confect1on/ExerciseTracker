using ExerciseTracker.MVVM.ViewModels;
using ExerciseTracker.MVVM.ViewModels.SessionOverview;

namespace SportMetricsViewer.Pages;

public partial class SummaryPage
{
    public SummaryPage(SummaryViewModel summaryViewModel)
    {
        InitializeComponent();
        BindingContext = summaryViewModel;
    }
}
