using MauiBlazorViews.Services;

namespace MauiBlazorViews.ViewModels
{
    public class SettingsViewModel(NavigationService navigation) : ViewModelBase
    {
        public string Title => "Settings";

        public void OpenWidgetSettings()
        {
            navigation.Push<WidgetSettingsViewModel>();
        }

        public void OpenAppSettings()
        {
            navigation.Push<AppSettingsViewModel>();
        }

        public void GoBack()
        {
            navigation.Pop();
        }
    }
}
