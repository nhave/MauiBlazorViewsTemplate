using MauiBlazorViews.Services;
using MauiBlazorViews.ViewModels;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorViews.Components.Layout
{
    public partial class NavMenu
    {
        [Inject]
        public NavigationService NavigationService { get; set; } = null!;

        private void NavigateHome()
        {
            NavigationService.PopToNearest<MainViewModel>();
        }

        private void NavigateCounter()
        {
            NavigationService.PushReplaceSafe<CounterViewModel, MainViewModel>();
        }

        private void NavigateWeather()
        {
            NavigationService.PushReplaceSafe<WeatherViewModel, MainViewModel>();
        }
    }
}
