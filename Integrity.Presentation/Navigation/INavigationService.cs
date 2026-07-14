using Integrity.Presentation.ViewModels;

namespace Integrity.Presentation.Navigation;

public interface INavigationService
{
    Task NavigateToAsync<TViewModel>()
        where TViewModel : ViewModelBase;
}