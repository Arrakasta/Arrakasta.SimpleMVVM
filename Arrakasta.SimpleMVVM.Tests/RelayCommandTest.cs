using Arrakasta.SimpleMVVM.Commands;

namespace Arrakasta.SimpleMVVM.Tests;

public class RelayCommandTest
{
    [Fact]
    public void RelayCommand_ShouldExecuteAction()
    {
        bool executed = false;
        var command = new RelayCommand(() => executed = true);
        command.Execute(null);
        Assert.True(executed);
    }
    [Fact]
    public void RelayCommand_ShouldNotExecuteWhenCanExecuteFalse()
    {
        bool executed = false;
        var command = new RelayCommand(() => executed = true, () => false);
        command.Execute(null);
        Assert.False(executed);
    }

    [Fact]
    public void RelayCommand_ShouldRaiseCanExecuteChanged()
    {
        bool canExecuteChangedRaised = false;
        var command = new RelayCommand(() => { }, () => true);
        command.CanExecuteChanged += (sender, args) => canExecuteChangedRaised = true;
        command.RaiseCanExecuteChanged();
        Assert.True(canExecuteChangedRaised);
    }

    [Fact]
    public void RelayCommand_ShouldThrowIfExecuteIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new RelayCommand(null!));
    }

    [Fact]
    public void RelayCommand_ShouldExecuteWithParameter()
    {
        bool executed = false;
        var command = new RelayCommand<object>(param => executed = true);
        command.Execute(new object());
        Assert.True(executed);
    }

    [Fact]
    public void RelayCommand_ShouldNotExecuteWithParameterWhenCanExecuteFalse()
    {
        bool executed = false;
        var command = new RelayCommand<object>(param => executed = true, param => false);
        command.Execute(new object());
        Assert.False(executed);
    }

    [Fact]
    public void RelayCommand_ShouldRaiseCanExecuteChangedWithParameter()
    {
        bool canExecuteChangedRaised = false;
        var command = new RelayCommand<object>(param => { }, param => true);
        command.CanExecuteChanged += (sender, args) => canExecuteChangedRaised = true;
        command.RaiseCanExecuteChanged();
        Assert.True(canExecuteChangedRaised);
    }
}