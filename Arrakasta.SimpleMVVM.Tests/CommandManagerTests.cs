using Arrakasta.SimpleMVVM.Commands;

namespace Arrakasta.SimpleMVVM.Tests;

public class CommandManagerTests
{
    [Fact]
    public void CommandManager_ShouldRaiseCanExecuteChanged()
    {
        var command = new RelayCommand(() => { }, () => true);
        bool canExecuteChangedRaised = false;
        command.CanExecuteChanged += (sender, args) => canExecuteChangedRaised = true;
        CommandManager.InvalidateRequerySuggested();
        Assert.True(canExecuteChangedRaised);
    }

    [Fact]
    public void CommandManager_ShouldNotRaiseCanExecuteChanged_WhenNoCommandsRegistered()
    {
        bool canExecuteChangedRaised = false;
        var command = new RelayCommand(() => { }, () => true);
        command.CanExecuteChanged += (sender, args) => canExecuteChangedRaised = true;
        CommandManager.Unregister(command);
        CommandManager.InvalidateRequerySuggested();
        Assert.False(canExecuteChangedRaised);
    }
}
