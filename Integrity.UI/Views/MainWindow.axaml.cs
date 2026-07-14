using Avalonia.Controls;
using Integrity.Presentation.ViewModels;

namespace Integrity.Views;

public partial class MainWindow : Window
{
    public MainWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}