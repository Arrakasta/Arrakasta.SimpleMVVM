using Arrakasta.SimpleMVVM.Messengers;

namespace Arrakasta.SimpleMVVM.Tests;

public class MessengerTest
{
    [Fact]
    public void Subscribe_ShouldAddHandler()
    {
        var messenger = new Messenger();
        bool messageReceived = false;
        messenger.Subscribe<string>(message => messageReceived = true);
        messenger.Send("Test Message");
        Assert.True(messageReceived);
    }
    [Fact]
    public void Unsubscribe_ShouldRemoveHandler()
    {
        var messenger = new Messenger();
        Action<string> handler = message => { };
        messenger.Subscribe(handler);
        messenger.Unsubscribe(handler);
        messenger.Send("Test Message"); // Should not throw
    }

    [Fact]
    public void DefaultMessenger_ShouldBeSingleton()
    {
        Messenger.Default.ClearSubscriptions();
        var messenger1 = Messenger.Default;
        var messenger2 = Messenger.Default;
        Assert.Same(messenger1, messenger2);
    }
    [Fact]
    public void DefaultMessenger_ShouldSendMessages()
    {
        Messenger.Default.ClearSubscriptions();
        bool messageReceived = false;
        Messenger.Default.Subscribe<string>(message => messageReceived = true);
        Messenger.Default.Send("Test Message");
        Assert.True(messageReceived);
    }
}
