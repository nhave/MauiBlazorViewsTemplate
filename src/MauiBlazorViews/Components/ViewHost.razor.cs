using MauiBlazorViews.Components.Layout;
using MauiBlazorViews.Services;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace MauiBlazorViews.Components
{
    public partial class ViewHost : IDisposable
    {
        [Inject]
        public NavigationStore NavigationStore { get; set; } = null!;

        [Inject]
        public ViewLocator ViewLocator { get; set; } = null!;

        private readonly Type DefaultLayout = typeof(MainLayout);
        private Type? CurrentViewType;
        private Type? CurrentLayoutType;
        private Dictionary<string, object>? CurrentParameters;

        protected override void OnInitialized()
        {
            NavigationStore.StateChanged += OnViewChanged;
            Refresh();
        }

        private void OnViewChanged()
        {
            Refresh();
            InvokeAsync(StateHasChanged);
        }

        private void Refresh()
        {
            var vm = NavigationStore.Current;

            if (vm == null)
                return;

            CurrentViewType =
                ViewLocator.Resolve(vm.GetType());

            CurrentLayoutType =
                ResolveLayout(CurrentViewType);

            CurrentParameters = new()
            {
                ["ViewModel"] = vm
            };
        }

        private Dictionary<string, object> GetLayoutParameters()
        {
            var layoutParams = new Dictionary<string, object>
            {
                ["Body"] = new RenderFragment(builder =>
                {
                    builder.OpenComponent(0, CurrentViewType!);
                    if (CurrentParameters != null)
                    {
                        foreach (var param in CurrentParameters)
                        {
                            builder.AddAttribute(1, param.Key, param.Value);
                        }
                    }
                    builder.CloseComponent();
                })
            };

            return layoutParams;
        }

        private Type? ResolveLayout(Type? componentType)
        {
            if (componentType == null)
                return DefaultLayout;

            // Check for LayoutAttribute which is set by @layout directive
            var layoutAttribute = componentType.GetCustomAttribute<LayoutAttribute>();
            if (layoutAttribute != null)
            {
                return layoutAttribute.LayoutType;
            }

            // Return default layout if no explicit layout is found
            return DefaultLayout;
        }

        public void Dispose()
        {
            NavigationStore.StateChanged -= OnViewChanged;
        }
    }
}
