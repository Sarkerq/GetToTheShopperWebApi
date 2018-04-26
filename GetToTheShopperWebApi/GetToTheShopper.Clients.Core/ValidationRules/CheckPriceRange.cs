using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GetToTheShopper.Clients.Core.ValidationRules
{

    public class CheckPriceRange : ValidationRule
    {
        private double min;
        private double max = double.MaxValue;
        private string message;
        public CheckPriceRange()
        {
        }

        public double Min
        {
            get { return min; }
            set { min = value; }
        }

        public double Max
        {
            get { return max; }
            set { max = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double age = 0;

            try
            {

                if (((string)value).Length == 0)
                    return new ValidationResult(false, "Please put correct number");
                else
                    age = double.Parse(((String)value).Replace('.', ','));
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Please put correct number");
            }

            if ((age < Min) || (age > Max))
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
