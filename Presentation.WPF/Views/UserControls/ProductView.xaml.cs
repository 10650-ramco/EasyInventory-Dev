using System.Windows;
using System.Windows.Controls;

namespace Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
            //SideDrawer.Visibility = Visibility.Collapsed;
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            SideDrawer.Visibility = Visibility.Visible;
            MainContent.Opacity = 0.25;
            //Inventory.Opacity = 0.5;
            SideDrawer.Opacity = 1;
            DrawerOverlay.Visibility = Visibility.Visible;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SideDrawer.Visibility = Visibility.Collapsed;
            MainContent.Opacity = 1;
            DrawerOverlay.Visibility = Visibility.Collapsed;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add save logic here
            SideDrawer.Visibility = Visibility.Collapsed;
            MainContent.Opacity = 1;
            DrawerOverlay.Visibility = Visibility.Collapsed;
        }
    }
}
