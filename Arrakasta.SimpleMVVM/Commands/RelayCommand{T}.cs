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
        _execute((T)parameter!);
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke((T)parameter!) ?? true;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}