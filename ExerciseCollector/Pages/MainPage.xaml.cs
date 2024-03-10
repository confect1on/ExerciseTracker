using System.Text.Json;
using System.Text.Json.Serialization;
using ExerciseCollector.Entities;
using ExerciseCollector.MVVM.ViewModels;

namespace ExerciseCollector.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        BindingContext = new ExerciseEntrantTypeViewModel();
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        var opt = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        await using var stream = await FileSystem.OpenAppPackageFileAsync("exercises.json");
        var exercises = await JsonSerializer.DeserializeAsync<List<Exercise>>(stream, opt);
        await Navigation.PushAsync(new GenderPage());
    }
}
