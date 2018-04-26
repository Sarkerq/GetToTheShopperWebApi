using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GetToTheShopper.Clients.Core.ValidationRules
{
    public class CheckName : ValidationRule
    {
        private string message;
        public CheckName()
        {
        }
        
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
           if (((string)value).Length == 0)
            {
                return new ValidationResult(false, message);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }

    }
}
