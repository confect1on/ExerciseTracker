using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Infrastructure;
using SportMetricsViewer.MVVM.ViewModels;

namespace SportMetricsViewer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit();
        builder.Services
            .AddTransient<IExercisesRepository, ResourceExercisesRepository>();
        
        builder.Services
            .AddTransient<GenderViewModel>()
            .AddTransient<ExerciseEntrantTypeViewModel>()
            .AddTransient<ResultsViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
