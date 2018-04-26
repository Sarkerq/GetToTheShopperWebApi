using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using MaterialDesignThemes.Wpf;
using GetToTheShopper.Clients.Core.ViewModel;
using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.Services;

namespace GetToTheShopper.Clients.Client.ViewModel
{
    public class ReceiptElementViewModel : ViewModelBase
    {
        private ReceiptProductDTO receiptProduct;
        private String acceptDialogText;

        private ReceiptProductService service;

        //Properties
        public ReceiptProductDTO ReceiptProduct
        {
            get { return receiptProduct; }
            set { SetProperty(ref receiptProduct, value); }
        }

        public String AcceptDialogText
        {
            get { return acceptDialogText; }
            set { SetProperty(ref acceptDialogText, value); }
        }

        //Constructors
        private void initialize(String acceptDialogText)
        {
            AcceptDialogText = acceptDialogText;
            service = new ReceiptProductService();
        }

        //for editing
        public ReceiptElementViewModel(ReceiptProductDTO _receiptsProduct, String acceptDialogText = "OK")
        {
            ReceiptProduct = _receiptsProduct;
            initialize(acceptDialogText);
        }

        //for creating
        public ReceiptElementViewModel(ReceiptDTO receipts, ProductDTO product, String acceptDialogText = "OK")
        {
            receiptProduct = new ReceiptProductDTO { ProductId = product.Id, ReceiptId = receipts.Id };
            initialize(acceptDialogText);
        }

        //functions called by dialogs in receipts
        public bool AddProductToList()
        {
           return service.AddProductToList(receiptProduct);
        }

        public bool EditQuantity()
        {
            return service.UpdateProductOnList(receiptProduct);
        }

        public bool DeleteProductFromList()
        {
            return service.DeleteProductFromList(receiptProduct);
        }
    }
}
