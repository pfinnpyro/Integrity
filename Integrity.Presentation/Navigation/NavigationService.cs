using Integrity.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity.Presentation.Navigation;

public class NavigationService (ShellViewModel shell, IServiceProvider serviceProvider) : INavigationService
{
    private readonly ViewModelBase _shell;
    
    public async Task NavigateToAsync<TViewModel>(
        Func<TViewModel, Task> initializer)
        where TViewModel : ViewModelBase
    {
        var viewModel = serviceProvider.GetRequiredService<TViewModel>();
        
        await initializer(viewModel);
        
        shell.CurrentWorkspace = viewModel;
    }
}