using Arrakasta.SimpleMVVM.Commands;
using Arrakasta.SimpleMVVM.Messengers;
using System.Windows.Input;

namespace Arrakasta.SimpleMVVM.Tests;

public class ViewModelBaseTest
{
    [Fact]
    public void Set_ShouldRaisePropertyChanged_WhenValueChanges()
    {
        var viewModel = new TestViewModel();
        bool propertyChangedRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(TestViewModel.TestProperty))
            {
                propertyChangedRaised = true;
            }
        };
        viewModel.TestProperty = "New Value";
        Assert.True(propertyChangedRaised);
        Assert.Equal("New Value", viewModel.TestProperty);
    }

    [Fact]
    public void Set_ShouldNotRaisePropertyChanged_WhenValueDoesNotChange()
    {
        var viewModel = new TestViewModel
        {
            TestProperty = "Initial Value"
        };
        bool propertyChangedRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(TestViewModel.TestProperty))
            {
                propertyChangedRaised = true;
            }
        };
        viewModel.TestProperty = "Initial Value"; // No change
        Assert.False(propertyChangedRaised);
    }

    [Fact]
    public void Set_ShouldNotifyMainViewModel_WhenMessageIsSent()
    {
        string? receivedMessage = null;
        Messenger.Default.Subscribe<string>(message => receivedMessage = message);
        var mainViewModel = new TestViewModel
        {
            Message = "Hello from TestViewModel"
        };
        mainViewModel.SendMessageCommand.Execute(null);
        Assert.Equal("Hello from TestViewModel", receivedMessage);
    }


    private class TestViewModel : ViewModelBase
    {
        private string? _testProperty;

        public string? TestProperty
        {
            get => _testProperty;
            set => Set(ref _testProperty, value);
        }

        private string _message;

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public ICommand SendMessageCommand { get; }

        public TestViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage);
        }

        private void SendMessage()
        {
            Messenger.Send(Message);
        }
    }
}
