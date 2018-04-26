using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GetToTheShopper.Clients.Client.Helpers
{
    public class ReceiptToTotalPriceConverter : IValueConverter
    {
        ReceiptProductService service = new ReceiptProductService();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReceiptDTO)
            {
                return service.GetTotalReceiptPrice((value as ReceiptDTO).Id);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return "";
        }
    }
}
