using SportMetricsViewer.MVVM.ViewModels;

namespace SportMetricsViewer.Pages;

public partial class SaveSessionPage
{
    public SaveSessionPage(SaveSessionViewModel saveSessionViewModel)
    {
        InitializeComponent();
        BindingContext = saveSessionViewModel;
    }
}
