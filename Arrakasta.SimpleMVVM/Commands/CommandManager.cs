using System.Windows.Input;

namespace Arrakasta.SimpleMVVM.Commands;

public static class CommandManager
{
    private static readonly HashSet<WeakReference<ICommand>> Commands = new();

    public static void Register(ICommand? command)
    {
        if (command == null) return;
        Commands.Add(new WeakReference<ICommand>(command));
    }

    public static void Unregister(ICommand command)
    {
        foreach (var weakRef in Commands.Where(wr => wr.TryGetTarget(out var cmd) && cmd == command).ToList())
        {
            Commands.Remove(weakRef);
        }
    }

    public static void InvalidateRequerySuggested()
    {
        foreach (var weakRef in Commands.ToList())
        {
            if (weakRef.TryGetTarget(out var command))
            {
                (command as IRaiseCanExecuteChanged)?.RaiseCanExecuteChanged();
            }
            else
            {
                Commands.Remove(weakRef); // Clean dead
            }
        }
    }
}
