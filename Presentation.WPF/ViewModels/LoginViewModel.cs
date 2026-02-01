using Application.DTOs;
using Application.Interfaces;
using Presentation.WPF.Infrastructure;
using Presentation.WPF.Services;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public sealed class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IWindowService _windowService;

        public LoginViewModel(IUserService userService, IWindowService windowService)
        {
            _userService = userService;
            _windowService = windowService;
            LoginCommand = new AsyncRelayCommand(LoginAsync, CanLogin);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                if (SetProperty(ref _userName, value))
                    LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    LoginCommand.RaiseCanExecuteChanged();
            }
        }


        public AsyncRelayCommand LoginCommand { get; }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(UserName)
                && !string.IsNullOrWhiteSpace(Password);
        }

        public async Task LoginAsync()
        {
            var request = new LoginRequestDto
            {
                UserName = UserName.Trim(),
                Password = Password
            };

            var result = await _userService.ValidateLoginAsync(request);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Error!, "Login Failed",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //MessageBox.Show("Login successful", "Success",
            //    MessageBoxButton.OK, MessageBoxImage.Information);

            // ✅ SUCCESS
            _windowService.ShowMainWindow();
            _windowService.CloseLoginWindow();
        }
    }
}