using CommunityToolkit.Maui;
using ExerciseTracker.MVVM.Abstractions;
using ExerciseTracker.MVVM.ViewModels;
using SportMetricsViewer.Implementations;
using SportMetricsViewer.Pages;

namespace SportMetricsViewer;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPages(this IServiceCollection services) => services
        .AddTransientWithShellRoute<GenderPage, GenderViewModel>(GenderViewModel.NavigationRoute)
        .AddTransientWithShellRoute<ExerciseEntrantTypePage, ExerciseEntrantTypeViewModel>(ExerciseEntrantTypeViewModel.NavigationRoute)
        .AddTransientWithShellRoute<SaveSessionPage, SaveSessionViewModel>(SaveSessionViewModel.NavigationRoute)
        .AddTransientWithShellRoute<SummaryPage, SummaryViewModel>(SummaryViewModel.NavigationRoute);
    
    public static IServiceCollection AddViewModels(this IServiceCollection services) => services
        .AddSingleton<SettingsViewModel>();

    public static IServiceCollection AddPlatformServices(this IServiceCollection services) => services
        .AddSingleton<INavigationService, ShellNavigationService>();
}
