using Arrakasta.SimpleMVVM.Messengers;

namespace Arrakasta.SimpleMVVM.Tests;

public class MessengerConcurrencyTests
{
    [Fact]
    public async Task Concurrent_Send_ShouldDeliverMessages()
    {
        var messenger = new Messenger();
        int received = 0;
        messenger.Subscribe<string>(_ => Interlocked.Increment(ref received));

        var tasks = Enumerable.Range(0, 100).Select(_ => Task.Run(() => messenger.Send("msg")));
        await Task.WhenAll(tasks);

        Assert.Equal(100, received);
    }

    [Fact]
    public async Task Concurrent_Subscribe_Unsubscribe_Send_ShouldBeThreadSafe()
    {
        var messenger = new Messenger();
        int received = 0;

        var tasks = Enumerable.Range(0, 100).Select(_ => Task.Run(() =>
        {
            Action<string> handler = _ => Interlocked.Increment(ref received);
            messenger.Subscribe(handler);
            messenger.Send("test");
            messenger.Unsubscribe(handler);
        }));

        await Task.WhenAll(tasks);

        Assert.True(received >= 100);
    }
}
