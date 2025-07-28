using System.Windows.Input;

namespace Arrakasta.SimpleMVVM.Commands;

public class AsyncRelayCommand<T> : ICommand, IRaiseCanExecuteChanged
{
    private readonly Func<T, Task> _execute;
    private readonly Func<T, bool>? _canExecute;
    private bool _isExecuting;

    public event EventHandler? CanExecuteChanged;

    public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
        CommandManager.Register(this);
    }

    public bool CanExecute(object? parameter)
    {
        var param = CastParameter(parameter);
        return !_isExecuting && (_canExecute?.Invoke(param) ?? true);
    }

    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter)) return;
        var param = CastParameter(parameter);
        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();
            await _execute(param);
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
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

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
