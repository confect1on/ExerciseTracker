using CommunityToolkit.Maui;
using SportMetricsViewer.MVVM.ViewModels;
using SportMetricsViewer.Pages;

namespace SportMetricsViewer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPages(this IServiceCollection services) => services
        .AddTransientWithShellRoute<GenderPage, GenderViewModel>(GenderViewModel.NavigationRoute)
        .AddTransientWithShellRoute<ExerciseEntrantTypePage, ExerciseEntrantTypeViewModel>(ExerciseEntrantTypeViewModel.NavigationRoute)
        .AddTransientWithShellRoute<SaveSessionPage, SaveSessionViewModel>(SaveSessionViewModel.NavigationRoute)
        .AddTransientWithShellRoute<SummaryPage, SummaryViewModel>(SummaryViewModel.NavigationRoute);

    public static IServiceCollection AddViewModels(this IServiceCollection services) => services
        .AddSingleton<SettingsViewModel>();
}
