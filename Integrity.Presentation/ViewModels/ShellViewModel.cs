using CommunityToolkit.Mvvm.ComponentModel;

namespace Integrity.Presentation.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ViewModelBase? _currentWorkspace;
}