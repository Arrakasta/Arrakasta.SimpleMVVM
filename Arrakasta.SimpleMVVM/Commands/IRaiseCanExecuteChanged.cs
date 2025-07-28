using System.Windows.Input;

namespace Arrakasta.SimpleMVVM.Commands;

public interface IRaiseCanExecuteChanged : ICommand
{
    void RaiseCanExecuteChanged();
}
