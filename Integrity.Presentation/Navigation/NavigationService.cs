using Integrity.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity.Presentation.Navigation;

public class NavigationService (ShellViewModel shell, IServiceProvider serviceProvider) : INavigationService
{
    private readonly ViewModelBase _shell;
    
    public Task NavigateToAsync<TViewModel>()
        where TViewModel : ViewModelBase
    {
        var viewModel = serviceProvider.GetRequiredService<TViewModel>();
        shell.CurrentWorkspace = viewModel;
        
        return Task.CompletedTask;
    }
}