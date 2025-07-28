using System.Windows.Input;

namespace Arrakasta.SimpleMVVM.Commands;

public static class CommandManager
{
    private static readonly HashSet<WeakReference<ICommand>> Commands = new();
    private static readonly object Sync = new();

    public static void Register(ICommand? command)
    {
        if (command == null) return;
        lock (Sync)
        {
            Commands.Add(new WeakReference<ICommand>(command));
        }
    }

    public static void Unregister(ICommand command)
    {
        lock (Sync)
        {
            foreach (var weakRef in Commands.Where(wr => wr.TryGetTarget(out var cmd) && cmd == command).ToList())
            {
                Commands.Remove(weakRef);
            }
        }
    }

    public static void InvalidateRequerySuggested()
    {
        List<WeakReference<ICommand>> snapshot;
        lock (Sync)
        {
            snapshot = Commands.ToList();
        }

        var dead = new List<WeakReference<ICommand>>();

        foreach (var weakRef in snapshot)
        {
            if (weakRef.TryGetTarget(out var command))
            {
                (command as IRaiseCanExecuteChanged)?.RaiseCanExecuteChanged();
            }
            else
            {
                dead.Add(weakRef);
            }
        }

        if (dead.Count > 0)
        {
            lock (Sync)
            {
                foreach (var weakRef in dead)
                {
                    Commands.Remove(weakRef); // Clean dead
                }
            }
        }
    }
}
