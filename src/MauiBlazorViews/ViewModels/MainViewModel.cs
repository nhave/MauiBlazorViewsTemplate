using MauiBlazorViews.Services;

namespace MauiBlazorViews.ViewModels
{
    public class MainViewModel(NavigationService navigation) : ViewModelBase
    {
        public string Title => "Forside";

        public bool TestBool { get; private set; } = true;

        public async Task Test()
        {
            TestBool = !TestBool;
        }

        public void OpenCounter()
        {
            navigation.Push<CounterViewModel>();
        }

        public void OpenWeather()
        {
            navigation.Push<WeatherViewModel>(w =>
            {
                w.WasConfigured = true;
            });
        }

        public void OpenSettings()
        {
            navigation.Push<SettingsViewModel>();
        }
    }
}
