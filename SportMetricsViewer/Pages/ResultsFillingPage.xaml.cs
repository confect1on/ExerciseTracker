using Microsoft.Extensions.Logging;
using SportMetricsViewer.MVVM.ViewModels;

namespace SportMetricsViewer.Pages;

public partial class ResultsFillingPage
{
    private readonly ILogger<ResultsFillingPage> _logger;

    public ResultsFillingPage(ResultsViewModel resultsViewModel, ILogger<ResultsFillingPage> logger)
    {
        _logger = logger;
        InitializeComponent();
        BindingContext = resultsViewModel;
    }
}
