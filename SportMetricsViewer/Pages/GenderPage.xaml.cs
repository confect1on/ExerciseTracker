using ExerciseTracker.MVVM.ViewModels;

namespace SportMetricsViewer.Pages;

public partial class GenderPage
{
    public GenderPage(GenderViewModel genderViewModel)
    {
        InitializeComponent();
        BindingContext = genderViewModel;
    }
}
