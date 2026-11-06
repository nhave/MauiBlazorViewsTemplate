using MauiBlazorViews.Services;

namespace MauiBlazorViews.ViewModels
{
    public class WeatherViewModel(NavigationService navigation) : ViewModelBase
    {
        public string Title => "Weather";

        public bool WasConfigured { get; set; } = false;

        public WeatherForecast[]? Forecasts { get; private set; }

        public async Task LoadWeatherForecastsAsync()
        {
            if (Forecasts is not null)
                return;

            // Simulate asynchronous loading to demonstrate a loading indicator
            await Task.Delay(500);

            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            Forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray();
        }

        public void Back()
        {
            navigation.Pop();
        }

        public void BackToHome()
        {
            navigation.PopToRoot();
        }

        public class WeatherForecast
        {
            public DateOnly Date { get; set; }
            public int TemperatureC { get; set; }
            public string? Summary { get; set; }
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
