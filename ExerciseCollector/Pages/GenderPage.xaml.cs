namespace ExerciseCollector.Pages;

public partial class GenderPage : ContentPage
{
    public GenderPage()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ExerciseCollectorPage());
    }
}
