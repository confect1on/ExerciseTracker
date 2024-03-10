using ExerciseCollector.Pages;

namespace ExerciseCollector;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Routing.RegisterRoute("gender", typeof(GenderPage));
        Routing.RegisterRoute("exerciseCollector", typeof(ExerciseCollectorPage));
        InitializeComponent();
    }
}
