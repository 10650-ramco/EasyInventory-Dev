using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Infrastructure
{
    public sealed class AsyncRelayCommand<T> : ICommand
    {
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool>? _canExecute;
        private bool _isExecuting;

        public event EventHandler? CanExecuteChanged;

        public AsyncRelayCommand(
            Func<T, Task> execute,
            Func<T, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_isExecuting)
                return false;

            if (parameter is T value)
                return _canExecute?.Invoke(value) ?? true;

            return false;
        }

        public async void Execute(object? parameter)
        {
            if (parameter is not T value || !CanExecute(parameter))
                return;

            try
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();

                await _execute(value).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
