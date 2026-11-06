using MauiBlazorViews.Services;
using MauiBlazorViews.ViewModels;
using System.Reflection;

namespace MauiBlazorViews.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers view models and configures view mappings for views.
        /// </summary>
        /// <remarks>Non-abstract classes derived from <see cref="ViewModelBase"/> in the executing
        /// assembly are registered as transient services. A singleton <see cref="ViewLocator"/> is configured with
        /// mappings derived by replacing the <c>ViewModel</c> suffix with <c>View</c>.</remarks>
        /// <param name="services">The service collection to which the view models and view locator are added.</param>
        /// <returns>The service collection with the registered view models and view locator.</returns>
        public static IServiceCollection AddViewModelsAndViews(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var viewModelBaseType = typeof(ViewModelBase);

            // Find all types that inherit from ViewModelBase
            var viewModelTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(viewModelBaseType))
                .ToList();

            // Register each ViewModel as Transient and build a mapping
            var viewMappings = new Dictionary<Type, string>();

            foreach (var viewModelType in viewModelTypes)
            {
                // Register the ViewModel as Transient
                services.AddTransient(viewModelType);

                // Derive the View name from ViewModel name
                // e.g., MainViewModel -> MainView
                var viewName = viewModelType.Name.Replace("ViewModel", "View");
                viewMappings[viewModelType] = viewName;
            }

            // Create a factory that configures the ViewLocator with all mappings
            services.AddSingleton<ViewLocator>(provider =>
            {
                var locator = new ViewLocator();
                RegisterViewMappings(locator, viewMappings, assembly);
                return locator;
            });

            return services;
        }

        /// <summary>
        /// Registers view mappings discovered in the specified assembly.
        /// </summary>
        /// <remarks>The view namespace is discovered from the assembly. Each view model mapping is
        /// registered when the corresponding view type is found.</remarks>
        /// <param name="locator">The view locator to configure.</param>
        /// <param name="viewMappings">The mappings between view model types and view type names.</param>
        /// <param name="assembly">The assembly containing the view types.</param>
        private static void RegisterViewMappings(ViewLocator locator, Dictionary<Type, string> viewMappings, Assembly assembly)
        {
            // Dynamically discover the Views namespace by finding compiled View types in the assembly
            // This approach doesn't require a marker class and works with actual View components
            var viewNamespace = DiscoverViewNamespace(assembly);

            foreach (var kvp in viewMappings)
            {
                var viewModelType = kvp.Key;
                var viewTypeName = kvp.Value;
                var fullViewTypeName = $"{viewNamespace}.{viewTypeName}";

                // Try to find the View type by name
                var viewType = assembly.GetType(fullViewTypeName);

                if (viewType != null)
                {
                    locator.Register(viewModelType, viewType);
                }
                // If View type is not found, it might be a Razor component that's compiled differently
                // In that case, the naming convention is still useful for routing/component resolution
            }
        }

        /// <summary>
        /// Finds the namespace containing the first concrete class whose name ends with "View".
        /// </summary>
        /// <param name="assembly">The assembly to search for a view type.</param>
        /// <returns>The namespace of the first matching view type, or `null` if no matching type is found.</returns>
        private static string? DiscoverViewNamespace(Assembly assembly)
        {
            // Find the first type that ends with "View" (excluding ViewModelBase and similar)
            // This will be used to determine the Views namespace
            var viewType = assembly.GetTypes()
                .FirstOrDefault(t => t.Name.EndsWith("View") && 
                                      !t.Name.Contains("ViewModel") &&
                                      t.IsClass && 
                                      !t.IsAbstract);

            return viewType?.Namespace;
        }
    }
}
