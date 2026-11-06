using MauiBlazorViews.Services;

namespace MauiBlazorViews
{
    public partial class MainPage : ContentPage
    {
        private readonly NavigationStore _navigationStore;

        public MainPage()
        {
            InitializeComponent();
            _navigationStore = App.Current?.Handler?.MauiContext?.Services.GetRequiredService<NavigationStore>() 
                ?? throw new InvalidOperationException("NavigationStore is not available");
        }

        protected override bool OnBackButtonPressed()
        {
            // If we can pop (not at root), handle the back button
            if (_navigationStore.CanPop)
            {
                _navigationStore.Pop();
                return true;
            }

            // Allow default behavior to close the app when at root
            return base.OnBackButtonPressed();
        }
    }
}
