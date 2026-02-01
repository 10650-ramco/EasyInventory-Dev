using Presentation.WPF.ViewModels;

namespace EasyInventory.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        //private readonly IProductRepository _repo;

        //public ObservableCollection<ProductModel> Products { get; } = new();

        //public ICommand LoadCommand { get; }

        //public ProductViewModel(IProductRepository repo)
        //{
        //    _repo = repo;
        //    LoadCommand = new RelayCommand<LoadProductsParams>(async p =>
        //        await LoadAsync(p.Page, p.PageSize));
        //}

        //private async Task LoadAsync(int page, int pageSize)
        //{
        //    Products.Clear();
        //    foreach (var p in await _repo.GetPagedAsync(page, pageSize))
        //        Products.Add((ProductModel)p);
        //}
    }
}


//using System.Collections.ObjectModel;
//using System.Windows;
//using EasyInventory.Commands;
//using EasyInventory.Data.Repositories.Interfaces;
//using EasyInventory.Models;

//namespace EasyInventory.ViewModels
//{
//    public class ProductViewModel : BaseViewModel
//    {
//        private readonly IProductRepository _productRepository;
//        private readonly ICategoryRepository _categoryRepository;
//        private ObservableCollection<Product> _products;
//        private ObservableCollection<Category> _categories;
//        private Product _selectedProduct;
//        private Category _selectedCategory;
//        private string _searchText;
//        private string _filterCategory;
//        private int _totalItems;
//        private int _lowStockCount;
//        private int _outOfStockCount;
//        private decimal _totalValue;
//        private bool _isLoading;
//        private bool _isDrawerOpen;
//        private bool _isEditMode;
//        private string _drawerTitle;
//        private bool _hasErrors;

//        public ProductViewModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
//        {
//            _productRepository = productRepository;
//            _categoryRepository = categoryRepository;
//            _products = new ObservableCollection<Product>();
//            _categories = new ObservableCollection<Category>();

//            AddNewItemCommand = new RelayCommand(_ => OpenAddNewItemDrawer());
//            SaveItemCommand = new RelayCommand(_ => SaveItem(), _ => !HasErrors);
//            CancelCommand = new RelayCommand(_ => CloseDrawer());
//            DeleteItemCommand = new RelayCommand<int>(DeleteItem);
//            EditItemCommand = new RelayCommand<int>(EditItem);
//            RefreshCommand = new RelayCommand(_ => LoadDataAsync());
//            FilterByCategoryCommand = new RelayCommand<string>(FilterByCategory);

//            LoadDataAsync();
//        }

//        public ObservableCollection<Product> Products
//        {
//            get => _products;
//            set => SetProperty(ref _products, value);
//        }

//        public ObservableCollection<Category> Categories
//        {
//            get => _categories;
//            set => SetProperty(ref _categories, value);
//        }

//        public Product SelectedProduct
//        {
//            get => _selectedProduct;
//            set => SetProperty(ref _selectedProduct, value);
//        }

//        public Category SelectedCategory
//        {
//            get => _selectedCategory;
//            set => SetProperty(ref _selectedCategory, value);
//        }

//        public string SearchText
//        {
//            get => _searchText;
//            set
//            {
//                if (SetProperty(ref _searchText, value))
//                {
//                    SearchAsync();
//                }
//            }
//        }

//        public string FilterCategory
//        {
//            get => _filterCategory;
//            set => SetProperty(ref _filterCategory, value);
//        }

//        public int TotalItems
//        {
//            get => _totalItems;
//            set => SetProperty(ref _totalItems, value);
//        }

//        public int LowStockCount
//        {
//            get => _lowStockCount;
//            set => SetProperty(ref _lowStockCount, value);
//        }

//        public int OutOfStockCount
//        {
//            get => _outOfStockCount;
//            set => SetProperty(ref _outOfStockCount, value);
//        }

//        public decimal TotalValue
//        {
//            get => _totalValue;
//            set => SetProperty(ref _totalValue, value);
//        }

//        public bool IsLoading
//        {
//            get => _isLoading;
//            set => SetProperty(ref _isLoading, value);
//        }

//        public bool IsDrawerOpen
//        {
//            get => _isDrawerOpen;
//            set => SetProperty(ref _isDrawerOpen, value);
//        }

//        public bool IsEditMode
//        {
//            get => _isEditMode;
//            set => SetProperty(ref _isEditMode, value);
//        }

//        public string DrawerTitle
//        {
//            get => _drawerTitle;
//            set => SetProperty(ref _drawerTitle, value);
//        }

//        public bool HasErrors
//        {
//            get => _hasErrors;
//            set => SetProperty(ref _hasErrors, value);
//        }

//        public RelayCommand AddNewItemCommand { get; }
//        public RelayCommand SaveItemCommand { get; }
//        public RelayCommand CancelCommand { get; }
//        public RelayCommand<int> DeleteItemCommand { get; }
//        public RelayCommand<int> EditItemCommand { get; }
//        public RelayCommand RefreshCommand { get; }
//        public RelayCommand<string> FilterByCategoryCommand { get; }

//        private async void LoadDataAsync()
//        {
//            try
//            {
//                IsLoading = true;
//                var products = await _productRepository.GetAllProductsAsync();
//                var categories = await _categoryRepository.GetAllCategoriesAsync();

//                Products = new ObservableCollection<Product>(products);
//                Categories = new ObservableCollection<Category>(categories);

//                await UpdateStatisticsAsync();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//            finally
//            {
//                IsLoading = false;
//            }
//        }

//        private async Task UpdateStatisticsAsync()
//        {
//            try
//            {
//                TotalItems = await _productRepository.GetTotalProductCountAsync();
//                LowStockCount = await _productRepository.GetLowStockCountAsync(10);
//                OutOfStockCount = await _productRepository.GetOutOfStockCountAsync();
//                TotalValue = await _productRepository.GetTotalInventoryValueAsync();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error updating statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private async void SearchAsync()
//        {
//            if (string.IsNullOrEmpty(SearchText))
//            {
//                LoadDataAsync();
//                return;
//            }

//            try
//            {
//                IsLoading = true;
//                var products = await _productRepository.SearchProductsAsync(SearchText);
//                Products = new ObservableCollection<Product>(products);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error searching: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//            finally
//            {
//                IsLoading = false;
//            }
//        }

//        private async void FilterByCategory(string category)
//        {
//            if (string.IsNullOrEmpty(category))
//            {
//                LoadDataAsync();
//                return;
//            }

//            try
//            {
//                IsLoading = true;
//                var products = await _productRepository.GetProductsByCategoryAsync(category);
//                Products = new ObservableCollection<Product>(products);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error filtering: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//            finally
//            {
//                IsLoading = false;
//            }
//        }

//        private void OpenAddNewItemDrawer()
//        {
//            SelectedProduct = new Product { LowStockThreshold = 10 };
//            IsEditMode = false;
//            DrawerTitle = "Add New Item";
//            HasErrors = false;
//            IsDrawerOpen = true;
//        }

//        private async void EditItem(int productId)
//        {
//            try
//            {
//                SelectedProduct = await _productRepository.GetProductByIdAsync(productId);
//                if (SelectedProduct != null)
//                {
//                    IsEditMode = true;
//                    DrawerTitle = "Edit Item";
//                    HasErrors = false;
//                    IsDrawerOpen = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private async void SaveItem()
//        {
//            if (SelectedProduct == null)
//            {
//                MessageBox.Show("No product selected.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
//                return;
//            }

//            if (!ValidateProduct(SelectedProduct))
//            {
//                MessageBox.Show("Please fix validation errors.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
//                HasErrors = true;
//                return;
//            }

//            try
//            {
//                if (SelectedProduct.ProductId == 0)
//                {
//                    await _productRepository.AddProductAsync(SelectedProduct);
//                    MessageBox.Show("Product added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
//                }
//                else
//                {
//                    await _productRepository.UpdateProductAsync(SelectedProduct);
//                    MessageBox.Show("Product updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
//                }

//                LoadDataAsync();
//                CloseDrawer();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error saving product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private async void DeleteItem(int productId)
//        {
//            var result = MessageBox.Show(
//                "Are you sure you want to delete this product?",
//                "Confirm Delete",
//                MessageBoxButton.YesNo,
//                MessageBoxImage.Question);

//            if (result != MessageBoxResult.Yes)
//                return;

//            try
//            {
//                await _productRepository.DeleteProductAsync(productId);
//                MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
//                LoadDataAsync();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void CloseDrawer()
//        {
//            IsDrawerOpen = false;
//            SelectedProduct = null;
//            HasErrors = false;
//        }

//        private bool ValidateProduct(Product product)
//        {
//            if (string.IsNullOrWhiteSpace(product.ProductName) || product.ProductName.Length < 3)
//                return false;

//            if (string.IsNullOrWhiteSpace(product.Category))
//                return false;

//            if (string.IsNullOrWhiteSpace(product.Unit))
//                return false;

//            if (product.Price <= 0)
//                return false;

//            if (product.Stock < 0)
//                return false;

//            return true;
//        }
//    }
//}