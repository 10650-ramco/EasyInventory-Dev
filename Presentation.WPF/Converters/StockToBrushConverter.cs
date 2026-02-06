using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Presentation.WPF.Converters
{
    public class StockToBrushConverter : IValueConverter
    {
        public int LowStockThreshold { get; set; } = 10;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Brushes.Transparent;

            if (!int.TryParse(value.ToString(), out int stock))
                return Brushes.Transparent;

            if (stock == 0)
                return Brushes.Red;

            if (stock <= LowStockThreshold)
                return Brushes.Orange;

            return Brushes.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
