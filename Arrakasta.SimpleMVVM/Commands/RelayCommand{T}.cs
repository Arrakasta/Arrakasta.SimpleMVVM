using System.Windows.Input;

namespace Arrakasta.SimpleMVVM.Commands;

public class RelayCommand<T>(Action<T> execute, Func<T, bool>? canExecute = null) : ICommand
{
    private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public void Execute(object? parameter)
    {
        if (!CanExecute(parameter))
        {
            return;
        }

        _execute(CastParameter(parameter));
    }

    public bool CanExecute(object? parameter)
    {
        var param = CastParameter(parameter);
        return canExecute?.Invoke(param) ?? true;
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
