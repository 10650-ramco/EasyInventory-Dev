using System.Collections.ObjectModel;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Application.Models
{
    public class MenuItemModel
    {
        public string Title { get; set; }
        public ICommand Command { get; set; }
        public PackIconKind? Icon { get; set; }
        public ObservableCollection<MenuItemModel> Children { get; set; }
            = new();
        
        public bool HasChildren => Children.Count > 0;
    }
}
