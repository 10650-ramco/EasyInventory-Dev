
using Application.DTOs;
using Application.Interfaces;
using MaterialDesignThemes.Wpf;
using Presentation.WPF.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Presentation.WPF.Views.UserControls;

namespace Presentation.WPF.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;

        public ObservableCollection<CustomerDto> Customers { get; } = new();

        private CustomerDto _editableCustomer = new();
        public CustomerDto EditableCustomer
        {
            get => _editableCustomer;
            set
            {
                if (SetProperty(ref _editableCustomer, value))
                {
                    OnPropertyChanged(nameof(DrawerTitle));
                }
            }
        }

        private bool _isDrawerOpen;
        public bool IsDrawerOpen
        {
            get => _isDrawerOpen;
            set
            {
                if (SetProperty(ref _isDrawerOpen, value))
                {
                    // Reset validation errors or logic if needed
                }
            }
        }

        public string DrawerTitle => EditableCustomer.CustomerId == 0 ? "Add New Customer" : "Edit Customer";

        // Pagination
        private int _pageIndex = 1;
        private const int PageSize = 10;
        private int _totalPages;
        public string PageDisplay => $"{_pageIndex} / {Math.Max(1, _totalPages)}";

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    _ = LoadCustomersAsync(1);
                }
            }
        }

        // Statistics
        private int _totalCustomers;
        public int TotalCustomers
        {
            get => _totalCustomers;
            set => SetProperty(ref _totalCustomers, value);
        }

        // Snackbar
        public ISnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue();

        // Commands
        public ICommand OpenAddDrawerCommand { get; }
        public ICommand OpenEditDrawerCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CloseDrawerCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }

        public CustomersViewModel(ICustomerService customerService)
        {
            _customerService = customerService;

            OpenAddDrawerCommand = new RelayCommand(OpenAddDrawer);
            OpenEditDrawerCommand = new RelayCommand<CustomerDto>(OpenEditDrawer);
            SaveCommand = new AsyncRelayCommand(SaveAsync);
            DeleteCommand = new AsyncRelayCommand<CustomerDto>(DeleteAsync);
            CloseDrawerCommand = new RelayCommand(() => IsDrawerOpen = false);

            NextPageCommand = new AsyncRelayCommand(
                () => LoadCustomersAsync(_pageIndex + 1),
                () => _pageIndex < _totalPages);

            PrevPageCommand = new AsyncRelayCommand(
                () => LoadCustomersAsync(_pageIndex - 1),
                () => _pageIndex > 1);

            _ = LoadCustomersAsync(1);
        }

        private async Task LoadCustomersAsync(int page)
        {
             try
            {
                var result = await _customerService.GetPagedAsync(page, PageSize, SearchText);
                
                Customers.Clear();
                foreach (var customer in result.Items)
                    Customers.Add(customer);

                _pageIndex = page;
                _totalPages = result.TotalPages;
                TotalCustomers = result.TotalCount;

                OnPropertyChanged(nameof(PageDisplay));
                (NextPageCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                (PrevPageCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                BoundMessageQueue.Enqueue($"Error loading customers: {ex.Message}");
            }
        }

        private void OpenAddDrawer()
        {
            EditableCustomer = new CustomerDto();
            IsDrawerOpen = true;
        }

        private void OpenEditDrawer(CustomerDto? customer)
        {
            if (customer == null) return;

            EditableCustomer = new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerCode = customer.CustomerCode,
                CustomerName = customer.CustomerName,
                CustomerType = customer.CustomerType,
                GSTIN = customer.GSTIN,
                PAN = customer.PAN,
                GSTRegistrationType = customer.GSTRegistrationType,
                IsGSTRegistered = customer.IsGSTRegistered,
                PlaceOfSupplyState = customer.PlaceOfSupplyState,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
            IsDrawerOpen = true;
        }

        private async Task SaveAsync()
        {
            // Simple Validation
            if (string.IsNullOrWhiteSpace(EditableCustomer.CustomerName))
            {
                BoundMessageQueue.Enqueue("Customer Name is required!");
                return;
            }

            try
            {
                // Uniqueness check for Code
                if (!string.IsNullOrWhiteSpace(EditableCustomer.CustomerCode))
                {
                    var isUnique = await _customerService.IsCodeUniqueAsync(
                        EditableCustomer.CustomerCode,
                        EditableCustomer.CustomerId == 0 ? null : EditableCustomer.CustomerId);
                    
                    if (!isUnique)
                    {
                        BoundMessageQueue.Enqueue($"Customer Code '{EditableCustomer.CustomerCode}' already exists!");
                        return;
                    }
                }

                if (EditableCustomer.CustomerId == 0)
                    await _customerService.AddAsync(EditableCustomer);
                else
                    await _customerService.UpdateAsync(EditableCustomer);

                BoundMessageQueue.Enqueue("Customer saved successfully.");
                IsDrawerOpen = false;
                await LoadCustomersAsync(_pageIndex);
            }
            catch (Exception ex)
            {
                BoundMessageQueue.Enqueue($"Error saving customer: {ex.Message}");
            }
        }

        private async Task DeleteAsync(CustomerDto? customer)
        {
            if (customer == null) return;
            
            var result = await DialogHost.Show(new ConfirmationDialog(), "RootDialog");
            if (result is bool confirmed && confirmed)
            {
                try
                {
                    await _customerService.DeleteAsync(customer.CustomerId);
                    BoundMessageQueue.Enqueue("Customer deleted.");
                    await LoadCustomersAsync(_pageIndex);
                }
                catch (Exception ex)
                {
                    BoundMessageQueue.Enqueue($"Error deleting customer: {ex.Message}");
                }
            }
        }
    }
}
