using Avalonia.Controls;
using Integrity.Presentation.ViewModels;

namespace Integrity.UI.Windows;

public partial class MainWindow : Window
{
    public MainWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}