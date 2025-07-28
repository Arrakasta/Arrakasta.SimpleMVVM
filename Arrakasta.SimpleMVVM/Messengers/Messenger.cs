namespace Arrakasta.SimpleMVVM.Messengers;

public class Messenger : IMessenger
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();
    private readonly object _lock = new();

    public static IMessenger Default { get; } = new Messenger();

    public void Subscribe<T>(Action<T> action)
    {
        lock (_lock)
        {
            if (!_handlers.TryGetValue(typeof(T), out var list))
            {
                _handlers[typeof(T)] = list = [];
            }
            list.Add(action);
        }
    }

    public void Unsubscribe<T>(Action<T> action)
    {
        lock (_lock)
        {
            if (_handlers.TryGetValue(typeof(T), out var list))
            {
                list.Remove(action);
                if (list.Count == 0)
                {
                    _handlers.Remove(typeof(T));
                }
            }
        }
    }

    public void Send<T>(T message)
    {
        List<Action<T>> handlers;
        lock (_lock)
        {
            if (!_handlers.TryGetValue(typeof(T), out var list)) return;
            handlers = list.OfType<Action<T>>().ToList();
        }

        foreach (var handler in handlers)
        {
            handler(message);
        }
    }

    public void ClearSubscriptions()
    {
        _handlers.Clear();
    }
}