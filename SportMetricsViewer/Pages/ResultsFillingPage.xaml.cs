using SportMetricsViewer.MVVM.ViewModels;

namespace SportMetricsViewer.Pages;

public partial class ResultsFillingPage
{
    public ResultsFillingPage(ResultsViewModel resultsViewModel)
    {
        InitializeComponent();
        BindingContext = resultsViewModel;
    }
}
