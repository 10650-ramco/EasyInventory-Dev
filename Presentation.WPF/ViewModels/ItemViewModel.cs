using Application.DTOs;
using Application.Interfaces;
using Presentation.WPF.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows.Input;

using MaterialDesignThemes.Wpf;
using Presentation.WPF.Views.UserControls;

namespace Presentation.WPF.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ObservableCollection<ProductDto> Products { get; } = new();
        public ObservableCollection<CategoryDto> Categories { get; } = new();

        private ProductDto _editableProduct = new();
        public ProductDto EditableProduct
        {
            get => _editableProduct;
            set => SetProperty(ref _editableProduct, value);
        }

        private bool _isDrawerOpen;
        public bool IsDrawerOpen
        {
            get => _isDrawerOpen;
            set
            {
                if (SetProperty(ref _isDrawerOpen, value))
                {
                    OnPropertyChanged(nameof(DrawerTitle));
                }
            }
        }

        public string DrawerTitle => EditableProduct.Id == 0 ? "Add New Item" : "Edit Item";

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
                    _ = LoadAsync(1);
                }
            }
        }

        // Statistics
        private int _totalItems;
        public int TotalItems { get => _totalItems; set => SetProperty(ref _totalItems, value); }

        private int _lowStockItems;
        public int LowStockItems { get => _lowStockItems; set => SetProperty(ref _lowStockItems, value); }

        private int _outOfStockItems;
        public int OutOfStockItems { get => _outOfStockItems; set => SetProperty(ref _outOfStockItems, value); }

        private decimal _totalValue;
        public decimal TotalValue { get => _totalValue; set => SetProperty(ref _totalValue, value); }

        // Commands
        public ICommand OpenAddDrawerCommand { get; }
        public ICommand OpenEditDrawerCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CloseDrawerCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }

        public ItemViewModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;

            OpenAddDrawerCommand = new RelayCommand(OpenAddDrawer);
            OpenEditDrawerCommand = new RelayCommand<ProductDto>(OpenEditDrawer);
            SaveCommand = new AsyncRelayCommand(SaveAsync);
            DeleteCommand = new AsyncRelayCommand<ProductDto>(DeleteAsync);
            CloseDrawerCommand = new RelayCommand(() => IsDrawerOpen = false);

            NextPageCommand = new AsyncRelayCommand(
                () => LoadAsync(_pageIndex + 1),
                () => _pageIndex < _totalPages);

            PrevPageCommand = new AsyncRelayCommand(
                () => LoadAsync(_pageIndex - 1),
                () => _pageIndex > 1);

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                var categoriesTask = _categoryService.GetAllAsync();
                var productsTask = LoadAsync(1);

                await Task.WhenAll(categoriesTask, productsTask);

                Categories.Clear();
                foreach (var category in await categoriesTask)
                    Categories.Add(category);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during ItemViewModel init: {ex.Message}");
            }
        }

        private async Task LoadAsync(int page)
        {
            try
            {
                var result = await _productService.GetPagedAsync(page, PageSize, SearchText);

                Products.Clear();
                foreach (var item in result.Items)
                    Products.Add(item);

                _pageIndex = page;
                _totalPages = result.TotalPages;

                OnPropertyChanged(nameof(PageDisplay));
                (NextPageCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                (PrevPageCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private void UpdateStatistics()
        {
            TotalItems = Products.Count;
            LowStockItems = Products.Count(p => p.Status == "Low Stock");
            OutOfStockItems = Products.Count(p => p.Status == "Out of Stock");
            TotalValue = Products.Sum(p => p.Price * p.Stock);
        }

        private void OpenAddDrawer()
        {
            EditableProduct = new ProductDto();
            IsDrawerOpen = true;
        }

        private void OpenEditDrawer(ProductDto? product)
        {
            if (product == null) return;

            EditableProduct = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                Unit = product.Unit,
                Price = product.Price,
                Stock = product.Stock,
                LowStockThreshold = product.LowStockThreshold
            };
            IsDrawerOpen = true;
        }

        private async Task SaveAsync()
        {
            if (EditableProduct.Id == 0)
                await _productService.AddAsync(EditableProduct);
            else
                await _productService.UpdateAsync(EditableProduct);

            IsDrawerOpen = false;
            await LoadAsync(_pageIndex);
        }

        private async Task DeleteAsync(ProductDto? product)
        {
            if (product == null) return;

            var result = await DialogHost.Show(new ConfirmationDialog(), "RootDialog");
            if (result is bool confirmed && confirmed)
            {
                await _productService.DeleteAsync(product.Id);
                await LoadAsync(_pageIndex);
            }
        }
    }
}
