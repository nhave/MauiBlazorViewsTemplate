namespace MauiBlazorViews.ViewModels
{
    /// <summary>
    /// Provides a base class for view models that notify subscribers when their state changes.
    /// </summary>
    public abstract class ViewModelBase
    {
        /// <summary>
        /// Allows subscribers to be notified when the state of the view model changes.
        /// </summary>
        public event Action? StateChanged;

        /// <summary>
        /// Raises the <see cref="StateChanged"/> event.
        /// </summary>
        protected void RaiseStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
