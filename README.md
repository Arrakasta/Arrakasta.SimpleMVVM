# Arrakasta.SimpleMVVM

Arrakasta.SimpleMVVM is a lightweight and easy-to-use MVVM (Model-View-ViewModel) framework for .NET 8 applications. It provides essential building blocks for implementing the MVVM pattern with minimal overhead.

## Features

- `ObservableObject`: Base class for property change notifications (INotifyPropertyChanged).
- `ViewModelBase`: Simplifies ViewModel creation and property management.
- `RelayCommand` and `RelayCommand<T>`: Flexible command implementations for UI actions.
- `AsyncRelayCommand` and `AsyncRelayCommand<T>`: Awaitable command variants for long-running operations.
- `Messenger` and `IMessenger`: Decoupled communication between ViewModels and components.
- `CommandManager`: Centralized command state management.
- Minimal dependencies and cross-platform support.

## Installation

Install via NuGet:

```
dotnet add package Arrakasta.SimpleMVVM
```

## Quick Start Example

```csharp
public class MainViewModel : ViewModelBase
{
    private string _message;
    public string Message
    {
        get => _message;
        set => Set(ref _message, value);
    }

    public ICommand SendMessageCommand { get; }

    public MainViewModel()
    {
        SendMessageCommand = new RelayCommand(SendMessage);
    }

    private void SendMessage()
    {
        Messenger.Send(Message);
    }
}
```

## Testing

Unit tests are available in the `Arrakasta.SimpleMVVM.Tests` project.

## License

This project is licensed under the MIT License.

## Contributing

Contributions are welcome! Please open issues or submit pull requests on GitHub.