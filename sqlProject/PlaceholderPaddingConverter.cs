using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace sqlProject
{
    internal class PlaceholderPaddingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            Thickness padding = (Thickness)value;
            padding.Right += 15;
            padding.Left += 15;
            return padding;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            Thickness padding = (Thickness)value;
            padding.Right -= 15;
            padding.Left -= 15;
            return padding;
        }
    }
}
