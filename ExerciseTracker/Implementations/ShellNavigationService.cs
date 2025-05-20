using ExerciseTracker.MVVM.Abstractions;
using ExerciseTracker.MVVM.ViewModels;
using SportMetricsViewer.Pages;

namespace SportMetricsViewer.Implementations;

internal sealed class ShellNavigationService : INavigationService
{
    public Task InitializeAsync() => NavigateToAsync(ExerciseEntrantTypeViewModel.NavigationRoute);

    public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
    {
        return routeParameters is null
            ? Shell.Current.GoToAsync(route)
            : Shell.Current.GoToAsync(route, routeParameters);
    }
}
