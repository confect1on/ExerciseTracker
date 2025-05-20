using SportMetricsViewer.Pages;

namespace SportMetricsViewer;

public partial class AppShell
{
    public AppShell()
    {
        Routing.RegisterRoute("gender", typeof(GenderPage));
        Routing.RegisterRoute("exerciseCollector", typeof(SaveSessionPage));
        Routing.RegisterRoute("summary", typeof(SummaryPage));
        InitializeComponent();
    }
}
