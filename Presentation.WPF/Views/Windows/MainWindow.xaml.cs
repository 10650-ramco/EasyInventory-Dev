using Presentation.WPF.ViewModels;
using System.Windows;

namespace Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ✅ Parameterless constructor REQUIRED for XAML
        public MainWindow()
        {
            InitializeComponent();
        }

        // ✅ USED by DI
        public MainWindow(MainViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

    }
}