using Application.DTOs;
using Application.Interfaces;
using Presentation.WPF.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private readonly IUserService _UserService;

        public UserViewModel(IUserService userService)
        {
            _UserService = userService;

            Users = new ObservableCollection<UserDto>();

            LoadUserCommand = new AsyncRelayCommand(LoadUserAsync);
            SaveUserCommand = new AsyncRelayCommand(SaveUserAsync, CanSave);
            DeleteUserCommand = new AsyncRelayCommand(DeleteUserAsync, CanDelete);
        }

        #region Properties

        public ObservableCollection<UserDto> Users { get; }

        private UserDto? _selectedUser;
        public UserDto? SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (SetProperty(ref _selectedUser, value))
                {
                    UserName  = value?.UserName ?? string.Empty;
                    Password = value?.Password ?? string.Empty;


                    SaveUserCommand.RaiseCanExecuteChanged();
                   // DeleteUserCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _userName  = string.Empty;
        public string UserName 
        {
            get => _userName ;
            set
            {
                if (SetProperty(ref _userName , value))
                    SaveUserCommand.RaiseCanExecuteChanged();
            }
        }


        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    SaveUserCommand.RaiseCanExecuteChanged();
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                    SaveUserCommand.RaiseCanExecuteChanged();
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (SetProperty(ref _lastName, value))
                    SaveUserCommand.RaiseCanExecuteChanged();
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                    SaveUserCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public AsyncRelayCommand LoadUserCommand { get; }
        public AsyncRelayCommand SaveUserCommand { get; }
        public AsyncRelayCommand DeleteUserCommand { get; }

        #endregion

        #region Logic

        private async Task LoadUserAsync()
        {
            Users.Clear();

            var users = await _UserService.GetAllUserAsync(CancellationToken.None);

            foreach (var user in users)
            {
                Users.Add(user); // ✅ FIX
                //Users.Add(UserMapper.ToDto(User)); // ✅ FIX
            }
        }

        private async Task SaveUserAsync()
        {
            UserDto dto = new()
            {
                Id = SelectedUser?.Id ?? 0,
                UserName = UserName.Trim(),
                Password = Password.Trim(),
                Name = Name.Trim(),
                LastName = LastName.Trim(),
                Email = Email.Trim()
            };

            if (SelectedUser == null)
            {
                var result = await _UserService.CreateUserAsync(dto);

                if (!result.IsSuccess)
                {
                    ShowError(result.Error);
                    return;
                }

                ShowInfo("User created successfully.");
            }
            else
            {
                var result = await _UserService.UpdateUserAsync(dto);

                if (!result.IsSuccess)
                {
                    ShowError(result.Error);
                    return;
                }

                ShowInfo("User updated successfully.");
            }

            await LoadUserAsync();
            ClearForm();
        }

        private async Task DeleteUserAsync()
        {
            if (SelectedUser == null)
                return;

            var confirmed = ShowConfirmation(
                "Are you sure you want to delete this User?");

            if (confirmed != MessageBoxResult.OK)
                return;

            var result = await _UserService.DeleteUserAsync(SelectedUser.Id);

            if (!result.IsSuccess)
            {
                ShowError(result.Error);
                return;
            }

            ShowInfo("User deleted successfully.");

            await LoadUserAsync();
            ClearForm();
        }

        #endregion

        #region Helpers

        private bool CanSave()
            => !string.IsNullOrWhiteSpace(UserName)
            && !string.IsNullOrWhiteSpace(Password);

        private bool CanDelete()
            => SelectedUser != null;

        private void ClearForm()
        {
            SelectedUser = null;
            UserName = string.Empty;
            Password = string.Empty;
        }

        private static void ShowInfo(string message)
        {
            MessageBox.Show(
                message,
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private static void ShowError(string error)
        {
            MessageBox.Show(
                error,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private static MessageBoxResult ShowConfirmation(string message)
        {
            return MessageBox.Show(
                message,
                "Confirmation",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question);
        }

        #endregion
    }
}
