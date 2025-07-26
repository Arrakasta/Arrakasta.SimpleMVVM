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

    private class TestViewModel : ViewModelBase
    {
        private string? _testProperty;
        public string? TestProperty
        {
            get => _testProperty;
            set => Set(ref _testProperty, value);
        }
    }
}