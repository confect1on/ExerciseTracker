using ExerciseCollector.Pages;

namespace ExerciseCollector;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
