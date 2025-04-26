using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SportMetricsViewer.Domain.Abstractions;
using SportMetricsViewer.Infrastructure;
using SportMetricsViewer.MVVM.ViewModels;
using SportMetricsViewer.Pages;

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
            .AddPages()
            .AddViewModels();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
