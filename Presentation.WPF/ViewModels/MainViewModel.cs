using Microsoft.Extensions.DependencyInjection;
using Presentation.WPF.Services;
using Presentation.WPF.Views;
using System.Windows;
using System.Windows.Input;
using WpfMvvmEfSample.Infrastructure;

namespace Presentation.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWindowService _windowService;

        public ICommand ToggleMenuCommand { get; }
        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowInventoryCommand { get; }
        public ICommand ShowSalesCommand { get; }
        public ICommand ShowPurchaseCommand { get; }
        public ICommand ShowReportsCommand { get; }
        public ICommand LogoffCommand { get; }

        public MainViewModel(
            IServiceProvider serviceProvider,
            IWindowService windowService)
        {
            _serviceProvider = serviceProvider;
            _windowService = windowService;

            ToggleMenuCommand = new RelayCommand(ToggleMenu);

            ShowDashboardCommand = new RelayCommand(() =>
                CurrentView = _serviceProvider.GetRequiredService<DashboardView>());

            ShowInventoryCommand = new RelayCommand(() =>
                CurrentView = _serviceProvider.GetRequiredService<ProductView>());

            ShowSalesCommand = new RelayCommand(() =>
                CurrentView = _serviceProvider.GetRequiredService<SalesView>());

            ShowPurchaseCommand = new RelayCommand(() =>
                CurrentView = _serviceProvider.GetRequiredService<PurchaseView>());

            ShowReportsCommand = new RelayCommand(() =>
                CurrentView = _serviceProvider.GetRequiredService<ReportsView>());

            LogoffCommand = new RelayCommand(Logoff);

            // Default view
            CurrentView = _serviceProvider.GetRequiredService<DashboardView>();
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        private bool _isMenuExpanded = true;
        public bool IsMenuExpanded
        {
            get => _isMenuExpanded;
            set
            {
                if (SetProperty(ref _isMenuExpanded, value))
                    OnPropertyChanged(nameof(MenuWidth));
            }
        }

        public GridLength MenuWidth =>
            IsMenuExpanded ? new GridLength(150) : new GridLength(60);

        private void ToggleMenu()
        {
            IsMenuExpanded = !IsMenuExpanded;
        }

        private void Logoff()
        {
            //_windowService.ShowLoginWindow();
            _windowService.CloseMainWindow();
        }
    }
}
