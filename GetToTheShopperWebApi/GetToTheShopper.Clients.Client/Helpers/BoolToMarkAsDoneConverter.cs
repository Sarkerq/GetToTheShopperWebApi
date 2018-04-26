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
    /// for binding of context menu item - shopping list done state
    /// </summary>
    class BoolToMarkAsDoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return "Mark as pending";
            }
            return "Mark as done";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
