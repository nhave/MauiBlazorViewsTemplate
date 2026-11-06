namespace MauiBlazorViews.Services
{
    /// <summary>
    /// Provides mappings between view model types and view types.
    /// </summary>
    public class ViewLocator
    {
        private readonly Dictionary<Type, Type> _map = [];

        /// <summary>
        /// Registers a view type for a view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The view model type.</typeparam>
        /// <typeparam name="TView">The view type.</typeparam>
        public void Register<TViewModel, TView>()
        {
            _map[typeof(TViewModel)] = typeof(TView);
        }

        /// <summary>
        /// Registers a view type for the specified view model type.
        /// </summary>
        /// <remarks>Replaces the existing view type if the view model type is already
        /// registered.</remarks>
        /// <param name="viewModelType">The view model type to associate with a view type.</param>
        /// <param name="viewType">The view type to associate with the view model type.</param>
        public void Register(Type viewModelType, Type viewType)
        {
            _map[viewModelType] = viewType;
        }

        /// <summary>
        /// Resolves the view type mapped to the specified view model type.
        /// </summary>
        /// <param name="viewModelType">The view model type to resolve.</param>
        /// <returns>The mapped view type, or `null` if no mapping exists.</returns>
        public Type? Resolve(Type viewModelType)
        {
            return _map.GetValueOrDefault(viewModelType);
        }
    }
}
