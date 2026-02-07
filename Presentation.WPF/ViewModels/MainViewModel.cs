using Application.Models;
using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WPF.Infrastructure;

namespace Presentation.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private object _currentView;
        public object CurrentView { get => _currentView; set { _currentView = value; OnPropertyChanged(); } }

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        private bool _isDarkMode;
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                OnPropertyChanged();
                
                var theme = _paletteHelper.GetTheme();
                theme.SetBaseTheme(value ? BaseTheme.Dark : BaseTheme.Light);
                _paletteHelper.SetTheme(theme);
            }
        }

        private bool _isSidebarCollapsed;
        public bool IsSidebarCollapsed { get => _isSidebarCollapsed; set { _isSidebarCollapsed = value; OnPropertyChanged(); } }

        public System.Windows.Input.ICommand ToggleSidebarCommand { get; }
        public ObservableCollection<MenuItemModel> Menus { get; }

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ToggleSidebarCommand = new RelayCommand(() => IsSidebarCollapsed = !IsSidebarCollapsed);

            Menus = new()
            {
                new() { Title="Dashboard", Icon=PackIconKind.ViewDashboard, Command=new RelayCommand(()=>NavigateTo<DashboardViewModel>()) },

                new()
                {
                    Title="Accounts",
                    Icon=PackIconKind.AccountGroup,
                    Children =
                    {
                        new(){ Title="Customers", Icon=PackIconKind.AccountTie, Command=new RelayCommand(()=>NavigateTo<CustomersViewModel>()) },
                        new(){ Title="Suppliers", Icon=PackIconKind.TruckDelivery, Command=new RelayCommand(()=>NavigateTo<SuppliersViewModel>()) },
                        new(){ Title="Account Ledger", Icon=PackIconKind.BookOpenPageVariant },
                        new(){ Title="Outstanding Receivables", Icon=PackIconKind.CashFast },
                        new(){ Title="Outstanding Payables", Icon=PackIconKind.CashMultiple },
                        new(){ Title="Opening Balance", Icon=PackIconKind.ScaleBalance }
                    }
                },

                new()
                {
                    Title="Inventory",
                    Icon=PackIconKind.PackageVariantClosed,
                    Children =
                    {
                        new(){ Title="Items", Icon=PackIconKind.Shape, Command=new RelayCommand(()=>NavigateTo<ItemViewModel>()) },
                        new(){ Title="Item Groups", Icon=PackIconKind.Group, Command=new RelayCommand(()=>NavigateTo<ItemGroupViewModel>()) },
                        new(){ Title="Units of Measure", Icon=PackIconKind.Ruler },
                        new(){ Title="Opening Stock", Icon=PackIconKind.PackageVariant },
                        new(){ Title="Stock Adjustment", Icon=PackIconKind.ClipboardEdit },
                        new(){ Title="Batch / Expiry", Icon=PackIconKind.CalendarClock }
                    }
                },

                new()
                {
                    Title="Sales",
                    Icon=PackIconKind.PointOfSale,
                    Children =
                    {
                        new(){ Title="Quotation", Icon=PackIconKind.FileDocumentOutline },
                        new(){ Title="Sales Order", Icon=PackIconKind.OrderAlphabeticalAscending },
                        new(){ Title="Delivery Challan", Icon=PackIconKind.TruckFast, Command=new RelayCommand(()=>NavigateTo<DeliveryChallanViewModel>()) },
                        new(){ Title="Merge Delivery Challan", Icon=PackIconKind.Merge },
                        new(){ Title="Sales Invoice", Icon=PackIconKind.Receipt, Command=new RelayCommand(()=>NavigateTo<SalesInvoiceViewModel>()) },
                        new(){ Title="Sales Return (Credit Note)", Icon=PackIconKind.ReceiptTextCheck }
                    }
                },

                new()
                {
                    Title="Purchase",
                    Icon=PackIconKind.Cart,
                    Children =
                    {
                        new(){ Title="Purchase Order", Icon=PackIconKind.OrderBoolAscendingVariant, Command=new RelayCommand(()=>NavigateTo<PurchaseOrderViewModel>()) },
                        new(){ Title="Goods Receipt (GRN)", Icon=PackIconKind.TruckCheck },
                        new(){ Title="Purchase Invoice", Icon=PackIconKind.ReceiptText, Command=new RelayCommand(()=>NavigateTo<PurchaseInvoiceViewModel>()) },
                        new(){ Title="Purchase Return (Debit Note)", Icon=PackIconKind.ReceiptTextRemove },
                        new(){ Title="Expenses", Icon=PackIconKind.CashMinus }
                    }
                },

                new()
                {
                    Title="Reports",
                    Icon=PackIconKind.ChartBar,
                    Children =
                    {
                         new(){ Title="Stock Summary", Icon=PackIconKind.ChartBox, Command=new RelayCommand(()=>NavigateTo<StockSummaryReportViewModel>()) },
                         new(){ Title="Stock Details", Icon=PackIconKind.ChartTimeline },
                         new(){ Title="Item-wise Sales Report", Icon=PackIconKind.ChartHistogram },
                         new(){ Title="Low Stock Report", Icon=PackIconKind.AlertCircleOutline },
                         new(){ Title="Sales Register", Icon=PackIconKind.BookOpen },
                         new(){ Title="Purchase Register", Icon=PackIconKind.BookOpenVariant },
                         new(){ Title="Outstanding Receivables", Icon=PackIconKind.CashFast },
                         new(){ Title="Outstanding Payables", Icon=PackIconKind.CashMultiple },
                         new(){ Title="GST Summary", Icon=PackIconKind.FilePercent },
                         new(){ Title="Profit & Loss", Icon=PackIconKind.Finance }
                    }
                },

                 new()
                {
                    Title="Settings",
                    Icon=PackIconKind.Cog,
                    Children =
                    {
                        new(){ Title="Company Details", Icon=PackIconKind.Domain, Command=new RelayCommand(()=>NavigateTo<CompanyDetailsViewModel>()) },
                        new(){ Title="Financial Year", Icon=PackIconKind.CalendarRange },
                        new(){ Title="GST & Tax Settings", Icon=PackIconKind.Percent },
                        new(){ Title="User Management", Icon=PackIconKind.AccountCog, Command=new RelayCommand(()=>NavigateTo<UserViewModel>()) },
                        new(){ Title="Roles & Permissions", Icon=PackIconKind.ShieldAccount },
                        new(){ Title="Invoice / Print Settings", Icon=PackIconKind.PrinterSettings },
                        new(){ Title="Backup & Restore", Icon=PackIconKind.DatabaseExport }
                    }
                },

                new() { Title="Logoff", Icon=PackIconKind.Logout }
            };

            NavigateTo<DashboardViewModel>();
        }

        private void NavigateTo<TViewModel>() where TViewModel : class
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            CurrentView = viewModel;
        }
    }
}
