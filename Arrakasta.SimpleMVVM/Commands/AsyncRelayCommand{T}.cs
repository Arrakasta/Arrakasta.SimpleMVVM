using System.Windows.Input;

namespace Arrakasta.SimpleMVVM.Commands;

public class AsyncRelayCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null) : ICommand
{
    private readonly Func<T, Task> _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    private bool _isExecuting;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        var param = CastParameter(parameter);
        return !_isExecuting && (canExecute?.Invoke(param) ?? true);
    }

    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter)) return;
        var param = CastParameter(parameter);
        try
        {
            _isExecuting = true;
            CommandManager.InvalidateRequerySuggested();
            await _execute(param);
        }
        finally
        {
            _isExecuting = false;
            CommandManager.InvalidateRequerySuggested();
        }
    }

    private static T CastParameter(object? parameter)
    {
        if (parameter is null)
        {
            return default!;
        }

        if (parameter is not T value)
        {
            throw new ArgumentException($"Expected parameter of type {typeof(T)}, got {parameter.GetType()}");
        }

        return value;
    }
}
