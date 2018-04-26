using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.ValidationRules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GetToTheShopper.Clients.Client.ValidationRules
{

    public class CheckIfReceiptNameExists : ValidationRule
    {
        private string message;
        private string firstSieveMessage;
        public CheckIfReceiptNameExists()
        {
            service = new ReceiptService();
        }
        private ReceiptService service;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string FirstSieveMessage
        {
            get { return firstSieveMessage; }
            set { firstSieveMessage = value; }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            CheckName firstSieveOfValidation = new CheckName();
            firstSieveOfValidation.Message = firstSieveMessage;
            ValidationResult result = firstSieveOfValidation.Validate(value, cultureInfo);
            if (!result.IsValid)
                return result;
            if (service.ReceiptNameAlreadyExists((string)value)) return new ValidationResult(false, message);

            return new ValidationResult(true, null);

        }

    }
}
