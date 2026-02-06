//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using WpfMvvmEfSample.Infrastructure;

//namespace Presentation.WPF.Views.UserControls
//{
//    /// <summary>
//    /// Interaction logic for PasswordInputControl.xaml
//    /// </summary>
//    public partial class PasswordInputControl : UserControl
//    {
//        public PasswordInputControl()
//        {
//            InitializeComponent();
//            ToggleCommand = new RelayCommand(() => IsPasswordVisible = !IsPasswordVisible);
//        }

//        public static readonly DependencyProperty PasswordProperty =
//            DependencyProperty.Register(nameof(Password),
//                typeof(string),
//                typeof(PasswordInputControl));

//        public string Password
//        {
//            get => (string)GetValue(PasswordProperty);
//            set => SetValue(PasswordProperty, value);
//        }

//        public static readonly DependencyProperty IsPasswordVisibleProperty =
//            DependencyProperty.Register(nameof(IsPasswordVisible),
//                typeof(bool),
//                typeof(PasswordInputControl));

//        public bool IsPasswordVisible
//        {
//            get => (bool)GetValue(IsPasswordVisibleProperty);
//            set => SetValue(IsPasswordVisibleProperty, value);
//        }

//        public ICommand ToggleCommand { get; }
//    }

//}
//}
