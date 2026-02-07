using Application.DTOs;
using Application.Interfaces;
using Presentation.WPF.Infrastructure;
using Presentation.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Presentation.WPF.ViewModels
{
    public class ProductViewModel : ViewModelBase
{
    private readonly IProductService _service;

    public ObservableCollection<ProductDto> Products { get; } = new();
    public ObservableCollection<CategoryDto> Categories { get; } = new();

    public GridLength DrawerWidth => IsDrawerOpen ? new GridLength(350) : new GridLength(0);

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
                OnPropertyChanged(nameof(DrawerWidth));
        }
    }

    public string DrawerTitle => EditableProduct.Id == 0 ? "Add Product" : "Edit Product";

    private int _pageIndex = 1;
    private const int PageSize = 10;
    private int _totalPages;

    public string PageDisplay => $"{_pageIndex} / {_totalPages}";

    // Commands
    public ICommand OpenAddDrawerCommand { get; }
    public ICommand OpenEditDrawerCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CloseDrawerCommand { get; }
    public ICommand NextPageCommand { get; }
    public ICommand PrevPageCommand { get; }

    public ProductViewModel(IProductService service)
    {
        _service = service;

        IsDrawerOpen = false;

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
        Categories.Clear();
        var categories = await _service.GetCategoriesAsync();
        foreach (var category in categories)
            Categories.Add(category);

        await LoadAsync(1);
    }

    private async Task LoadAsync(int page)
    {
        var result = await _service.GetPagedAsync(page, PageSize);

        Products.Clear();
        foreach (var item in result.Items)
            Products.Add(item);

        _pageIndex = page;
        _totalPages = result.TotalPages;

        OnPropertyChanged(nameof(PageDisplay));
    }

    private void OpenAddDrawer()
    {
        EditableProduct = new ProductDto();
        IsDrawerOpen = true;
        OnPropertyChanged(nameof(DrawerTitle));
    }

    private void OpenEditDrawer(ProductDto product)
    {
        EditableProduct = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            CategoryName = product.CategoryName,
            Price = product.Price,
            Stock = product.Stock
        };
        IsDrawerOpen = true;
        OnPropertyChanged(nameof(DrawerTitle));
    }

    private async Task SaveAsync()
    {
        if (EditableProduct.Id == 0)
            await _service.AddAsync(EditableProduct);
        else
            await _service.UpdateAsync(EditableProduct);

        IsDrawerOpen = false;
        await LoadAsync(_pageIndex);
    }

    private async Task DeleteAsync(ProductDto product)
    {
        if (product == null) return;
        await _service.DeleteAsync(product.Id);
        await LoadAsync(_pageIndex);
    }
}
}
