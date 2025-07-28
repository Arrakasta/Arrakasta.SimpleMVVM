namespace Arrakasta.SimpleMVVM.Commands;

public class RelayCommand<T> : IRaiseCanExecuteChanged
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool>? _canExecute;

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
    {
        _canExecute = canExecute;
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        CommandManager.Register(this);
    }

    public void Execute(object? parameter)
    {
        if (!CanExecute(parameter))
        {
            return;
        }

        _execute(CastParameter(parameter));
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke(CastParameter(parameter)) ?? true;

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

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
