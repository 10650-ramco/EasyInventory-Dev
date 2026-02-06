using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Presentation.WPF.Views
{
    public partial class InventoryView : UserControl
    {
        private const double DrawerWidth = 400;

        public InventoryView()
        {
            InitializeComponent();
        }

        // Add New Item button click
        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            OpenDrawer();
        }

        // Overlay click to close drawer
        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseDrawer();
        }

        // Cancel button click
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseDrawer();
        }

        // Save button click
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Implement save logic here
            CloseDrawer();
        }

        private void OpenDrawer()
        {
            DrawerOverlay.Visibility = Visibility.Visible;
            SideDrawer.Visibility = Visibility.Visible;

            // Slide drawer in
            var slideIn = new DoubleAnimation
            {
                From = DrawerWidth,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8
            };
            SideDrawerTransform.BeginAnimation(TranslateTransform.XProperty, slideIn);

            // Fade in overlay
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            DrawerOverlay.BeginAnimation(OpacityProperty, fadeIn);
        }

        private void CloseDrawer()
        {
            // Slide drawer out
            var slideOut = new DoubleAnimation
            {
                From = 0,
                To = DrawerWidth,
                Duration = TimeSpan.FromMilliseconds(300),
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8
            };
            slideOut.Completed += (s, e) =>
            {
                SideDrawer.Visibility = Visibility.Collapsed;
            };
            SideDrawerTransform.BeginAnimation(TranslateTransform.XProperty, slideOut);

            // Fade out overlay
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            fadeOut.Completed += (s, e) =>
            {
                DrawerOverlay.Visibility = Visibility.Collapsed;
            };
            DrawerOverlay.BeginAnimation(OpacityProperty, fadeOut);
        }
    }
}
