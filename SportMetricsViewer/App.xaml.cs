using SportMetricsViewer.Pages;

namespace SportMetricsViewer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
