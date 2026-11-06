using MauiBlazorViews.Services;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorViews.Components.Layout
{
    public partial class NavButton : IAsyncDisposable
    {
        [Inject]
        public NavigationStore NavigationStore { get; set; } = null!;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string? Class { get; set; }
        private string className => string.IsNullOrWhiteSpace(Class) ? "" : Class;

        [Parameter]
        public required Type ViewModelType { get; set; }

        [Parameter]
        public EventCallback OnNavigate { get; set; }

        private bool IsActive => NavigationStore.Current?.GetType() == ViewModelType;

        private string GetActiveClass() => IsActive ? "active" : "";

        protected override void OnInitialized()
        {
            NavigationStore.StateChanged += OnNavigationChanged;
        }

        private void OnNavigationChanged()
        {
            InvokeAsync(StateHasChanged);
        }

        private async Task OnClick()
        {
            if (!IsActive && OnNavigate.HasDelegate)
            {
                await OnNavigate.InvokeAsync();
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            NavigationStore.StateChanged -= OnNavigationChanged;
        }
    }
}
