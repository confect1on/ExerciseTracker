using ExerciseTracker.MVVM.ViewModels;

namespace SportMetricsViewer.Pages;

public partial class ExerciseEntrantTypePage
{
    public ExerciseEntrantTypePage(ExerciseEntrantTypeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
