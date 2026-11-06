using MauiBlazorViews.Services;

namespace MauiBlazorViews.ViewModels
{
    public class AppSettingsViewModel(NavigationService navigation) : ViewModelBase
    {
        public string Title => "App Settings";

        public void GoBack()
        {
            navigation.Pop();
        }
    }
}
