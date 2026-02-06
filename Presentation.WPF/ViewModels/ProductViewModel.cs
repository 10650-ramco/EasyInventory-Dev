using Application.DTOs;
using Application.Interfaces;
using Presentation.WPF.Infrastructure;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfMvvmEfSample.Infrastructure;

namespace Presentation.WPF.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly IProductService _productService;

        public ProductViewModel(IProductService productService)
        {
            _productService = productService;

            RefreshCommand = new AsyncRelayCommand(LoadAsync);
            AddNewItemCommand = new RelayCommand(OpenAddDrawer);
            EditItemCommand = new RelayCommand<int>(OpenEditDrawer);
            DeleteItemCommand = new AsyncRelayCommand<int>(DeleteAsync);
            SaveItemCommand = new AsyncRelayCommand(SaveAsync);
            CancelCommand = new RelayCommand(CloseDrawer);

            _ = LoadAsync();
        }

        public ObservableCollection<ProductDto> Products { get; } = new();

        private ProductDto _selectedProduct;
        public ProductDto SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public bool IsDrawerOpen { get; set; }
        public bool IsLoading { get; set; }

        public int TotalItems => Products.Count;
        public int LowStockCount => Products.Count(p => p.Status == "Low Stock");
        public int OutOfStockCount => Products.Count(p => p.Status == "Out of Stock");
        public decimal TotalValue => Products.Sum(p => p.Price * p.Stock);

        public string DrawerTitle => SelectedProduct?.ProductId == 0
            ? "Add Product"
            : "Edit Product";

        public ICommand RefreshCommand { get; }
        public ICommand AddNewItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SaveItemCommand { get; }
        public ICommand CancelCommand { get; }

        private async Task LoadAsync()
        {
            IsLoading = true;
            OnPropertyChanged(nameof(IsLoading));

            Products.Clear();
            var items = await _productService.GetProductsAsync();
            foreach (var item in items)
                Products.Add(item);

            OnPropertyChanged(nameof(TotalItems));
            OnPropertyChanged(nameof(LowStockCount));
            OnPropertyChanged(nameof(OutOfStockCount));
            OnPropertyChanged(nameof(TotalValue));

            IsLoading = false;
            OnPropertyChanged(nameof(IsLoading));
        }

        private void OpenAddDrawer()
        {
            SelectedProduct = new ProductDto();
            IsDrawerOpen = true;
            OnPropertyChanged(nameof(IsDrawerOpen));
        }

        private void OpenEditDrawer(int productId)
        {
            SelectedProduct = Products.First(p => p.ProductId == productId);
            IsDrawerOpen = true;
            OnPropertyChanged(nameof(IsDrawerOpen));
        }

        private async Task SaveAsync()
        {
            await _productService.SaveAsync(SelectedProduct);
            CloseDrawer();
            await LoadAsync();
        }

        private async Task DeleteAsync(int productId)
        {
            await _productService.DeleteAsync(productId);
            await LoadAsync();
        }

        private void CloseDrawer()
        {
            IsDrawerOpen = false;
            OnPropertyChanged(nameof(IsDrawerOpen));
        }
    }
}