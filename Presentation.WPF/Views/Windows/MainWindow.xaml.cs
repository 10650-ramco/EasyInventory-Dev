using Presentation.WPF.Views;
using System.Windows;

namespace Presentation.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(EmployeeView employeeView)
        {
            InitializeComponent();
            MainContent.Content = employeeView;
        }
    }
}
