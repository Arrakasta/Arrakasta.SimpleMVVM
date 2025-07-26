namespace Arrakasta.SimpleMVVM.Commands;

public class RelayCommand : IRaiseCanExecuteChanged
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _canExecute = canExecute;
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        CommandManager.Register(this);
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object? parameter)
    {
        if(!CanExecute(parameter)) return;
        _execute();
    }

    public event EventHandler? CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}