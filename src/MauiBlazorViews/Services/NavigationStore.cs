using MauiBlazorViews.ViewModels;

namespace MauiBlazorViews.Services
{
    /// <summary>
    /// Represents a navigation entry containing a view model and its parameters.
    /// </summary>
    /// <remarks>Navigation parameters are stored in the <see cref="Parameters"/> property.</remarks>
    /// <param name="viewModel">The view model associated with the navigation entry.</param>
    public sealed class NavigationEntry(ViewModelBase viewModel)
    {
        /// <summary>
        /// Gets the view model.
        /// </summary>
        public ViewModelBase ViewModel { get; } = viewModel;

        /// <summary>
        /// Gets the named parameters and their values.
        /// </summary>
        public Dictionary<string, object?> Parameters { get; } = [];
    }

    /// <summary>
    /// Manages the navigation history and provides access to the current and root view models.
    /// </summary>
    public sealed class NavigationStore
    {
        private readonly List<NavigationEntry> _history = [];

        /// <summary>
        /// Allows subscribers to be notified when the navigation state changes.
        /// </summary>
        public event Action? StateChanged;

        /// <summary>
        /// Gets the current view model.
        /// </summary>
        public ViewModelBase? Current =>
            _history.LastOrDefault()?.ViewModel;

        /// <summary>
        /// Gets the root view model.
        /// </summary>
        public ViewModelBase? Root =>
            _history.FirstOrDefault()?.ViewModel;

        /// <summary>
        /// Gets the navigation history.
        /// </summary>
        public IReadOnlyList<NavigationEntry> History =>
            _history.AsReadOnly();

        /// <summary>
        /// Gets the number of entries in the history.
        /// </summary>
        public int Count => _history.Count;

        /// <summary>
        /// Gets a value indicating whether the history can be popped.
        /// </summary>
        public bool CanPop => _history.Count > 1;

        /// <summary>
        /// Initializes navigation history with the specified root view model.
        /// </summary>
        /// <remarks>Clears the existing navigation history and notifies listeners that the state has
        /// changed.</remarks>
        /// <param name="rootViewModel">The root view model for the navigation history.</param>
        public void Initialize(ViewModelBase rootViewModel)
        {
            _history.Clear();
            _history.Add(new NavigationEntry(rootViewModel));

            StateChanged?.Invoke();
        }

        /// <summary>
        /// Adds a view model to the navigation history.
        /// </summary>
        /// <remarks>Raises the state-changed notification.</remarks>
        /// <param name="viewModel">The view model to add to the navigation history.</param>
        public void Push(ViewModelBase viewModel)
        {
            _history.Add(new NavigationEntry(viewModel));

            StateChanged?.Invoke();
        }

        /// <summary>
        /// Replaces the current navigation entry with the specified view model.
        /// </summary>
        /// <remarks>Adds the view model when the navigation history contains one or fewer entries;
        /// otherwise, replaces the current entry and signals that the state has changed.</remarks>
        /// <param name="viewModel">The view model to add or use as the replacement.</param>
        public void PushReplace(ViewModelBase viewModel)
        {
            if (_history.Count <= 1)
            {
                Push(viewModel);
                return;
            }

            _history[^1] = new NavigationEntry(viewModel);

            StateChanged?.Invoke();
        }

        /// <summary>
        /// Removes up to the specified number of entries from the history.
        /// </summary>
        /// <remarks>The initial history entry is retained.</remarks>
        /// <param name="count">The maximum number of history entries to remove.</param>
        public void Pop(int count = 1)
        {
            if (_history.Count <= 1)
                return;

            count = Math.Min(count, _history.Count - 1);

            _history.RemoveRange(
                _history.Count - count,
                count);

            StateChanged?.Invoke();
        }

        /// <summary>
        /// Removes all history entries except the root entry.
        /// </summary>
        public void PopToRoot()
        {
            if (_history.Count <= 1)
                return;

            _history.RemoveRange(1, _history.Count - 1);

            StateChanged?.Invoke();
        }

        /// <summary>
        /// Removes all view models after the nearest view model of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of view model to locate.</typeparam>
        public void PopToNearest<T>()
            where T : ViewModelBase
        {
            int index = _history.FindLastIndex(entry => entry.ViewModel is T);
            if (index == -1 || index == _history.Count - 1)
                return;
            _history.RemoveRange(index + 1, _history.Count - index - 1);
            StateChanged?.Invoke();
        }
    }
}
