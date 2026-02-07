using Application.DTOs;
using Application.Interfaces;
using Presentation.WPF.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Presentation.WPF.Views.UserControls;

namespace Presentation.WPF.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

            Employees = new ObservableCollection<EmployeeDto>();

            LoadCommand = new AsyncRelayCommand(LoadAsync);
            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync, CanDelete);
        }

        #region Properties

        public ObservableCollection<EmployeeDto> Employees { get; }

        private EmployeeDto? _selectedEmployee;
        public EmployeeDto? SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (SetProperty(ref _selectedEmployee, value))
                {
                    Name = value?.Name ?? string.Empty;
                    Department = value?.Department ?? string.Empty;

                    SaveCommand.RaiseCanExecuteChanged();
                    DeleteCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                    SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private string _department = string.Empty;
        public string Department
        {
            get => _department;
            set
            {
                if (SetProperty(ref _department, value))
                    SaveCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public AsyncRelayCommand LoadCommand { get; }
        public AsyncRelayCommand SaveCommand { get; }
        public AsyncRelayCommand DeleteCommand { get; }

        #endregion

        #region Logic

        private async Task LoadAsync()
        {
            Employees.Clear();

            var employees = await _employeeService.GetAllAsync(CancellationToken.None);

            foreach (var employee in employees)
            {
                Employees.Add(employee); // ✅ FIX
                //Employees.Add(EmployeeMapper.ToDto(employee)); // ✅ FIX
            }
        }

        private async Task SaveAsync()
        {
            EmployeeDto dto = new()
            {
                Id = SelectedEmployee?.Id ?? 0,
                Name = Name.Trim(),
                Department = Department.Trim()
            };

            if (SelectedEmployee == null)
            {
                var result = await _employeeService.CreateAsync(dto);

                if (!result.IsSuccess)
                {
                    ShowError(result.Error);
                    return;
                }

                ShowInfo("Employee created successfully.");
            }
            else
            {
                var result = await _employeeService.UpdateAsync(dto);

                if (!result.IsSuccess)
                {
                    ShowError(result.Error);
                    return;
                }

                ShowInfo("Employee updated successfully.");
            }

            await LoadAsync();
            ClearForm();
        }

        private async Task DeleteAsync()
        {
            if (SelectedEmployee == null)
                return;

            var result = await DialogHost.Show(new ConfirmationDialog(), "RootDialog");
            if (result is bool confirmed && confirmed)
            {
                var deleteResult = await _employeeService.DeleteAsync(SelectedEmployee.Id);

                if (!deleteResult.IsSuccess)
                {
                    ShowError(deleteResult.Error);
                    return;
                }

                ShowInfo("Employee deleted successfully.");

                await LoadAsync();
                ClearForm();
            }
        }

        #endregion

        #region Helpers

        private bool CanSave()
            => !string.IsNullOrWhiteSpace(Name)
            && !string.IsNullOrWhiteSpace(Department);

        private bool CanDelete()
            => SelectedEmployee != null;

        private void ClearForm()
        {
            SelectedEmployee = null;
            Name = string.Empty;
            Department = string.Empty;
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
