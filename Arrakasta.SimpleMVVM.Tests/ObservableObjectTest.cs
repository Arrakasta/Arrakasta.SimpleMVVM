namespace Arrakasta.SimpleMVVM.Tests;

public class ObservableObjectTest
{
    [Fact]
    public void SetProperty_ShouldRaisePropertyChanged_WhenValueChanges()
    {
        var obj = new TestObservableObject();
        bool propertyChangedRaised = false;
        obj.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(TestObservableObject.TestProperty))
            {
                propertyChangedRaised = true;
            }
        };
        obj.TestProperty = "New Value";
        Assert.True(propertyChangedRaised);
        Assert.Equal("New Value", obj.TestProperty);
    }

    [Fact]
    public void SetProperty_ShouldNotRaisePropertyChanged_WhenValueDoesNotChange()
    {
        var obj = new TestObservableObject
        {
            TestProperty = "Initial Value"
        };

        bool propertyChangedRaised = false;
        obj.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(TestObservableObject.TestProperty))
            {
                propertyChangedRaised = true;
            }
        };
        obj.TestProperty = "Initial Value"; // No change
        Assert.False(propertyChangedRaised);
    }

    private class TestObservableObject : ObservableObject
    {
        private string? _testProperty;
        public string? TestProperty
        {
            get => _testProperty;
            set
            {
                if (_testProperty == value) return;

                _testProperty = value;
                RaisePropertyChanged();
            }
        }
    }
}