using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GetToTheShopper.Clients.Client.Helpers
{
    /// <summary>
    /// For binding availability in shopping list
    /// </summary>
    class BoolToAvailabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Product out of sell";

            if (value is bool && (bool)value)
            {
                return "Available";
            }
            return "Not available";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
