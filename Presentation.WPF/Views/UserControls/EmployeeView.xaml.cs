using Presentation.WPF.ViewModels;
using System.Windows.Controls;

namespace Presentation.WPF.Views
{
    public partial class EmployeeView : UserControl
    {
        // ✅ Required by XAML
        public EmployeeView()
        {
            InitializeComponent();
        }

        // ✅ Used by DI
        public EmployeeView(EmployeeViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}