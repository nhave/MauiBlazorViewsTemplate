
using MauiBlazorViews.Services;

namespace MauiBlazorViews.ViewModels
{
    public class CounterViewModel(NavigationService navigation) : ViewModelBase
    {
        public string Title => "Counter";

        public int CurrentCount { get; private set; } = 0;

        public void IncrementCount()
        {
            CurrentCount++;
        }

        public void OpenWeather()
        {
            navigation.Push<WeatherViewModel>();
        }

        public void Back()
        {
            navigation.Pop();
        }
    }
}
