using MauiBlazorViews.Extensions;
using MauiBlazorViews.Services;
using MauiBlazorViews.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiBlazorViews
{
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
                });

            builder.Services.AddMauiBlazorWebView();

            // Automatically discover and register ViewModels and Views
            builder.Services.AddViewModelsAndViews();

            builder.Services.AddSingleton<NavigationStore>();
            builder.Services.AddSingleton<NavigationService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Initialize NavigationStore with MainViewModel as default
            var navigationStore = app.Services.GetRequiredService<NavigationStore>();
            var mainViewModel = app.Services.GetRequiredService<MainViewModel>();
            navigationStore.Initialize(mainViewModel);

            return app;
        }
    }
}

