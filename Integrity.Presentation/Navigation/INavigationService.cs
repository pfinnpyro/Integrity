using Integrity.Presentation.ViewModels;

namespace Integrity.Presentation.Navigation;

public interface INavigationService
{
    Task NavigateToAsync<TViewModel>(Func<TViewModel, Task> initializer)
        where TViewModel : ViewModelBase;
}