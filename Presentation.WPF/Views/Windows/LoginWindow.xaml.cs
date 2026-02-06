using Presentation.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool _isPasswordVisible = false;


        // ✅ Parameterless constructor REQUIRED for XAML
        public LoginWindow()
        {
            InitializeComponent();
        }

        // ✅ DI constructor
        public LoginWindow(LoginViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                await vm.LoginAsync();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
                vm.Password = ((PasswordBox)sender).Password;
        }
    }
}
