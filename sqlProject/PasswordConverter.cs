using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace sqlProject
{
    internal class PasswordConverter : IValueConverter
    {
        private char symbol = '\u25CF';

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string newValue = "";
            for (int i = 0; i < value.ToString().Length; ++i)
                newValue += symbol;
            return newValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
