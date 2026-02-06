using System;
using System.Windows.Input;

namespace Presentation.WPF.Infrastructure
{
    public sealed class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool>? _canExecute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(
            Action<T> execute,
            Func<T, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is T value)
                return _canExecute?.Invoke(value) ?? true;

            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is T value && CanExecute(parameter))
                _execute(value);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
