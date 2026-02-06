using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Presentation.WPF.Views
{
    public partial class ProductView : UserControl
    {
        private const double DrawerWidth = 250;

        public ProductView()
        {
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            OpenDrawer();
        }

        private void CancelDrawer_Click(object sender, RoutedEventArgs e)
        {
            CloseDrawer();
        }

        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseDrawer();
        }

        public void OpenDrawer()
        {
            SideDrawer.Visibility = Visibility.Visible;
            Overlay.Visibility = Visibility.Visible;

            // Slide in drawer
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
            var overlayFade = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            Overlay.BeginAnimation(OpacityProperty, overlayFade);

            // Fade in drawer content
            var contentFade = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400))
            {
                BeginTime = TimeSpan.FromMilliseconds(150)
            };
            DrawerContent.BeginAnimation(OpacityProperty, contentFade);
        }

        public void CloseDrawer()
        {
            // Slide out drawer
            var slideOut = new DoubleAnimation
            {
                From = 0,
                To = DrawerWidth,
                Duration = TimeSpan.FromMilliseconds(300),
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8
            };
            slideOut.Completed += (s, e) => SideDrawer.Visibility = Visibility.Collapsed;
            SideDrawerTransform.BeginAnimation(TranslateTransform.XProperty, slideOut);

            // Fade out overlay
            var overlayFade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            overlayFade.Completed += (s, e) => Overlay.Visibility = Visibility.Collapsed;
            Overlay.BeginAnimation(OpacityProperty, overlayFade);

            // Fade out drawer content
            var contentFade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200));
            DrawerContent.BeginAnimation(OpacityProperty, contentFade);
        }
    }
}
