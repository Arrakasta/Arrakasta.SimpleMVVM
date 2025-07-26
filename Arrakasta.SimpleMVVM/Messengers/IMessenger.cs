namespace Arrakasta.SimpleMVVM.Messengers;

public interface IMessenger
{
    void Subscribe<T>(Action<T> action);
    void Unsubscribe<T>(Action<T> action);
    void Send<T>(T message);
}