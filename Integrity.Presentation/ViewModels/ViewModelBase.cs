using CommunityToolkit.Mvvm.ComponentModel;

namespace Integrity.Presentation.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private ViewModelBase? currentPage;
}