using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GetToTheShopper.Clients.Client.Helpers
{
    /// <summary>
    /// for binding of color of availability
    /// </summary>
    class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converter = new System.Windows.Media.BrushConverter();
            Brush brush;
            if (value is bool && (bool)value)
            {
                brush = (Brush)converter.ConvertFromString("#4CAF50");
            }
            else
            {
                brush = (Brush)converter.ConvertFromString("#C62828");
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
