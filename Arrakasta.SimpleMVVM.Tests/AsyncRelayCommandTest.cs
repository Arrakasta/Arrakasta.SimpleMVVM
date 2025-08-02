using Arrakasta.SimpleMVVM.Commands;

namespace Arrakasta.SimpleMVVM.Tests;

public class AsyncRelayCommandTest
{
    [Fact]
    public async Task AsyncRelayCommand_ShouldExecuteAsyncAction()
    {
        var tcs = new TaskCompletionSource();
        var command = new AsyncRelayCommand(() =>
        {
            tcs.SetResult();
            return Task.CompletedTask;
        });

        command.Execute(null);
        await tcs.Task;
    }

    [Fact]
    public async Task AsyncRelayCommand_ShouldNotExecuteWhenCanExecuteFalse()
    {
        var tcs = new TaskCompletionSource();
        var command = new AsyncRelayCommand(() =>
        {
            tcs.SetResult();
            return Task.CompletedTask;
        }, () => false);

        command.Execute(null);

        await Task.Delay(100);
        Assert.False(tcs.Task.IsCompleted);
    }

    [Fact]
    public async Task AsyncRelayCommand_Generic_ShouldExecuteAsyncAction()
    {
        var tcs = new TaskCompletionSource();
        var command = new AsyncRelayCommand<int>(_ =>
        {
            tcs.SetResult();
            return Task.CompletedTask;
        });

        command.Execute(5);
        await tcs.Task;
    }

    [Fact]
    public async Task AsyncRelayCommand_Generic_ShouldNotExecuteWhenCanExecuteFalse()
    {
        var tcs = new TaskCompletionSource();
        var command = new AsyncRelayCommand<int>(_ =>
        {
            tcs.SetResult();
            return Task.CompletedTask;
        }, _ => false);

        command.Execute(5);
        await Task.Delay(100);
        Assert.False(tcs.Task.IsCompleted);
    }
}
