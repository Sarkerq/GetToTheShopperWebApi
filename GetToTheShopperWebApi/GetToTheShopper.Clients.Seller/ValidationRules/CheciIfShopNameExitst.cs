﻿using GetToTheShopper.Clients.Core.ValidationRules;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetToTheShopper.Clients.Core.Services;

namespace GetToTheShopper.Clients.Seller.ValidationRules
{
    public class CheckIfShopNameExists : ValidationRule
    {
        private string message;
        private string firstSieveMessage;
        private ShopService service;
        public CheckIfShopNameExists()
        {
            service = new ShopService();
        }

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
            if (service.ShopNameAlreadyExists((string)value)) return new ValidationResult(false, message);
            return new ValidationResult(true, null);

        }
    }
}
