using MauiBlazorViews.Services;

namespace MauiBlazorViews.ViewModels
{
    public class WidgetSettingsViewModel(NavigationService navigation) : ViewModelBase
    {
        public string Title => "Widget Settings";

        public void GoBack()
        {
            navigation.Pop();
        }
    }
}
