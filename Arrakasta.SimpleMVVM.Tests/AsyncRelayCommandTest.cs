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
    public void AsyncRelayCommand_ShouldRaiseCanExecuteChanged()
    {
        var command = new AsyncRelayCommand(() => Task.CompletedTask);
        bool raised = false;
        command.CanExecuteChanged += (_, _) => raised = true;
        command.RaiseCanExecuteChanged();
        Assert.True(raised);
    }

    [Fact]
    public async Task AsyncRelayCommand_Generic_ShouldExecuteAsyncAction()
    {
        var tcs = new TaskCompletionSource();
        var command = new AsyncRelayCommand<int>(i =>
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
        var command = new AsyncRelayCommand<int>(i =>
        {
            tcs.SetResult();
            return Task.CompletedTask;
        }, i => false);

        command.Execute(5);
        await Task.Delay(100);
        Assert.False(tcs.Task.IsCompleted);
    }

    [Fact]
    public void AsyncRelayCommand_Generic_ShouldRaiseCanExecuteChanged()
    {
        var command = new AsyncRelayCommand<int>(_ => Task.CompletedTask);
        bool raised = false;
        command.CanExecuteChanged += (_, _) => raised = true;
        command.RaiseCanExecuteChanged();
        Assert.True(raised);
    }
}
