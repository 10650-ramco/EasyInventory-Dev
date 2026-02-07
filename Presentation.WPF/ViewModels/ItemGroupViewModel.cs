using Application.DTOs;
using Application.Interfaces;
using MaterialDesignThemes.Wpf;
using Presentation.WPF.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Presentation.WPF.ViewModels
{
    public class ItemGroupViewModel : ViewModelBase
    {
        private readonly ICategoryService _categoryService;

        public ObservableCollection<CategoryDto> Categories { get; } = new();

        private CategoryDto _editableCategory = new();
        public CategoryDto EditableCategory
        {
            get => _editableCategory;
            set => SetProperty(ref _editableCategory, value);
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

        public string DrawerTitle => EditableCategory.CategoryId == 0 ? "Add New Category" : "Edit Category";

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
                    _ = LoadCategoriesAsync(1);
                }
            }
        }

        // Statistics
        private int _totalGroups;
        public int TotalGroups { get => _totalGroups; set => SetProperty(ref _totalGroups, value); }

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

        public ItemGroupViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            OpenAddDrawerCommand = new RelayCommand(OpenAddDrawer);
            OpenEditDrawerCommand = new RelayCommand<CategoryDto>(OpenEditDrawer);
            SaveCommand = new AsyncRelayCommand(SaveAsync);
            DeleteCommand = new AsyncRelayCommand<CategoryDto>(DeleteAsync);
            CloseDrawerCommand = new RelayCommand(() => IsDrawerOpen = false);

            NextPageCommand = new AsyncRelayCommand(
                () => LoadCategoriesAsync(_pageIndex + 1),
                () => _pageIndex < _totalPages);

            PrevPageCommand = new AsyncRelayCommand(
                () => LoadCategoriesAsync(_pageIndex - 1),
                () => _pageIndex > 1);

            _ = LoadCategoriesAsync(1);
        }

        private async Task LoadCategoriesAsync(int page)
        {
            try
            {
                var result = await _categoryService.GetPagedAsync(page, PageSize, SearchText);
                
                Categories.Clear();
                foreach (var category in result.Items)
                    Categories.Add(category);

                _pageIndex = page;
                _totalPages = result.TotalPages;
                TotalGroups = result.TotalCount;

                OnPropertyChanged(nameof(PageDisplay));
                (NextPageCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                (PrevPageCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                BoundMessageQueue.Enqueue($"Error loading categories: {ex.Message}");
            }
        }

        private void OpenAddDrawer()
        {
            EditableCategory = new CategoryDto();
            IsDrawerOpen = true;
        }

        private void OpenEditDrawer(CategoryDto? category)
        {
            if (category == null) return;

            EditableCategory = new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            IsDrawerOpen = true;
        }

        private async Task SaveAsync()
        {
            // Validation
            if (string.IsNullOrWhiteSpace(EditableCategory.CategoryName))
            {
                BoundMessageQueue.Enqueue("Category name is required!", "OK", () => { });
                return;
            }

            try
            {
                // Duplicate check
                var isUnique = await _categoryService.IsNameUniqueAsync(
                    EditableCategory.CategoryName, 
                    EditableCategory.CategoryId == 0 ? null : EditableCategory.CategoryId);

                if (!isUnique)
                {
                    BoundMessageQueue.Enqueue($"Category '{EditableCategory.CategoryName}' already exists!", "OK", () => { });
                    return;
                }

                if (EditableCategory.CategoryId == 0)
                    await _categoryService.AddAsync(EditableCategory);
                else
                    await _categoryService.UpdateAsync(EditableCategory);

                BoundMessageQueue.Enqueue($"Category '{EditableCategory.CategoryName}' saved successfully.");
                IsDrawerOpen = false;
                await LoadCategoriesAsync(_pageIndex);
            }
            catch (Exception ex)
            {
                BoundMessageQueue.Enqueue($"Error: {ex.Message}");
            }
        }

        private async Task DeleteAsync(CategoryDto? category)
        {
            if (category == null) return;
            try
            {
                await _categoryService.DeleteAsync(category.CategoryId);
                BoundMessageQueue.Enqueue("Category deleted.");
                await LoadCategoriesAsync(_pageIndex);
            }
            catch (Exception ex)
            {
                BoundMessageQueue.Enqueue($"Could not delete: {ex.Message}");
            }
        }
    }
}
