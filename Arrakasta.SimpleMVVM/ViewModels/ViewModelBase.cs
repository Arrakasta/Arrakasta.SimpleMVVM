using System.Runtime.CompilerServices;
using Arrakasta.SimpleMVVM.Messengers;

namespace Arrakasta.SimpleMVVM;

public class ViewModelBase(IMessenger? messenger = null) : ObservableObject
{
    protected readonly IMessenger Messenger = messenger ?? Arrakasta.SimpleMVVM.Messengers.Messenger.Default;

    protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        RaisePropertyChanged(propertyName);
        return true;
    }
}