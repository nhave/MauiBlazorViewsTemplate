using MauiBlazorViews.ViewModels;

namespace MauiBlazorViews.Services
{
    /// <summary>
    /// Provides navigation operations for view models.
    /// </summary>
    /// <param name="store">The store that maintains the navigation stack.</param>
    /// <param name="services">The service provider used to resolve view models.</param>
    public sealed class NavigationService(NavigationStore store, IServiceProvider services)
    {
        /// <summary>
        /// Creates and pushes a view model onto the store.
        /// </summary>
        /// <typeparam name="T">The type of view model to create and push.</typeparam>
        /// <param name="configure">An optional action that configures the view model before it is pushed.</param>
        public void Push<T>(Action<T>? configure = null)
            where T : ViewModelBase
        {
            var vm = services.GetRequiredService<T>();
            configure?.Invoke(vm);

            store.Push(vm);
        }

        /// <summary>
        /// Resolves, configures, and pushes a view model, replacing the current entry.
        /// </summary>
        /// <typeparam name="T">The type of view model to resolve and push.</typeparam>
        /// <param name="configure">An optional action used to configure the resolved view model.</param>
        public void PushReplace<T>(Action<T>? configure = null)
            where T : ViewModelBase
        {
            var vm = services.GetRequiredService<T>();
            configure?.Invoke(vm);

            store.PushReplace(vm);
        }

        /// <summary>
        /// Pushes a configured view model, replacing the current view model unless it is of type <typeparamref
        /// name="TNoReplace"/>.
        /// </summary>
        /// <typeparam name="TPush">The type of view model to push.</typeparam>
        /// <typeparam name="TNoReplace">The type of current view model that must not be replaced.</typeparam>
        /// <param name="configure">An optional action that configures the view model before it is pushed.</param>
        public void PushReplaceSafe<TPush, TNoReplace>(Action<TPush>? configure = null)
            where TPush : ViewModelBase
            where TNoReplace : ViewModelBase
        {
            var vm = services.GetRequiredService<TPush>();
            configure?.Invoke(vm);
            if (store.Current is TNoReplace)
            {
                store.Push(vm);
            }
            else
            {
                store.PushReplace(vm);
            }
        }

        /// <summary>
        /// Removes the specified number of items from the collection.
        /// </summary>
        /// <param name="count">The number of items to remove.</param>
        public void Pop(int count = 1)
        {
            store.Pop(count);
        }

        /// <summary>
        /// Pops all entries from the navigation stack except the root entry.
        /// </summary>
        public void PopToRoot()
        {
            store.PopToRoot();
        }

        /// <summary>
        /// Pops views from the navigation stack until the nearest view model of the specified type is reached.
        /// </summary>
        /// <typeparam name="T">The view model type to locate.</typeparam>
        public void PopToNearest<T>()
            where T : ViewModelBase
        {
            store.PopToNearest<T>();
        }
    }
}
