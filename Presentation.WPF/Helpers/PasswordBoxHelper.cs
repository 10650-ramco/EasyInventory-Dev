using System.Windows;
using System.Windows.Controls;

namespace Presentation.WPF.Helpers
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BoundPassword",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject obj)
            => (string)obj.GetValue(BoundPasswordProperty);

        public static void SetBoundPassword(DependencyObject obj, string value)
            => obj.SetValue(BoundPasswordProperty, value);

        private static void OnBoundPasswordChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox pb && pb.Password != (string)e.NewValue)
                pb.Password = (string)e.NewValue;
        }
    }

}
