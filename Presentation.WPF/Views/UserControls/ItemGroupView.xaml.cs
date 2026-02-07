using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Presentation.WPF.ViewModels;

namespace Presentation.WPF.Views
{
    public partial class ItemGroupView : UserControl
    {
        private const double DrawerWidth = 500;

        public ItemGroupView()
        {
            InitializeComponent();
            DataContextChanged += ItemGroupView_DataContextChanged;
        }

        private void ItemGroupView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is ItemGroupViewModel oldViewModel)
            {
                oldViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }

            if (e.NewValue is ItemGroupViewModel newViewModel)
            {
                newViewModel.PropertyChanged += ViewModel_PropertyChanged;
                // Sync initial state
                if (newViewModel.IsDrawerOpen)
                    OpenDrawer();
            }
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemGroupViewModel.IsDrawerOpen))
            {
                if (DataContext is ItemGroupViewModel viewModel)
                {
                    if (viewModel.IsDrawerOpen)
                        OpenDrawer();
                    else
                        CloseDrawer();
                }
            }
        }

        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ItemGroupViewModel viewModel)
            {
                viewModel.IsDrawerOpen = false;
            }
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
                Duration = TimeSpan.FromMilliseconds(400),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            SideDrawerTransform.BeginAnimation(TranslateTransform.XProperty, slideIn);

            // Fade in overlay
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400));
            DrawerOverlay.BeginAnimation(OpacityProperty, fadeIn);
        }

        private void CloseDrawer()
        {
            // Slide drawer out
            var slideOut = new DoubleAnimation
            {
                To = DrawerWidth,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            slideOut.Completed += (s, e) =>
            {
                SideDrawer.Visibility = Visibility.Collapsed;
                DrawerOverlay.Visibility = Visibility.Collapsed;
            };

            SideDrawerTransform.BeginAnimation(TranslateTransform.XProperty, slideOut);

            // Fade out overlay
            var fadeOut = new DoubleAnimation(0, TimeSpan.FromMilliseconds(300));
            DrawerOverlay.BeginAnimation(OpacityProperty, fadeOut);
        }
    }
}
