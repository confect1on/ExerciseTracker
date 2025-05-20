using CommunityToolkit.Maui;
using ExerciseTracker.Domain.Abstractions;
using ExerciseTracker.Domain.Services;
using ExerciseTracker.Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;

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
            .AddDataAccess()
            .AddDomainServices()
            .AddPages()
            .AddPlatformServices()
            .AddViewModels();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
