namespace Arrakasta.SimpleMVVM.Messengers;

public class Messenger : IMessenger
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public static IMessenger Default { get; } = new Messenger();

    public void Subscribe<T>(Action<T> action)
    {
        if (!_handlers.TryGetValue(typeof(T), out var list))
        {
            _handlers[typeof(T)] = list = [];
        }
        list.Add(action);
    }

    public void Unsubscribe<T>(Action<T> action)
    {
        if (_handlers.TryGetValue(typeof(T), out var list))
        {
            list.Remove(action);
        }
    }

    public void Send<T>(T message)
    {
        if (!_handlers.TryGetValue(typeof(T), out var list)) return;

        foreach (var handler in list.OfType<Action<T>>())
        {
            handler(message);
        }
    }

    public void ClearSubscriptions()
    {
        _handlers.Clear();
    }
}