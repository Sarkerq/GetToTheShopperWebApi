using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using GetToTheShopper.Clients.Core.ViewModel;
using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.View;
using GetToTheShopper.Clients.Client.View;

namespace GetToTheShopper.Clients.Client.ViewModel
{
    public class ReceiptViewModel : ViewModelBase
    {
        //commands
        public ICommand FilterProductsCommand { get; set; }
        public ICommand OpenAddProductToListDialogCommand { get; set; }
        public ICommand OpenEditProductQuantityOnListDialogCommand { get; set; }
        public ICommand OpenDeleteProductFromListDialogCommand { get; set; }
        public ICommand ReceiptInShopsCommand { get; set; }

        //viewmodels related with dialogs
        public ReceiptElementViewModel addProductToListVM;
        public ReceiptElementViewModel editProductQuantityOnListVM;
        public ReceiptElementViewModel deleteProductFromListVM;

        private NavigationViewModel ownerWindow;
        private String acceptDialogText;
        private bool noProductsFound;

        private ShopDTO selectedShop;

        private ReceiptService service;
        private ShopService shopService;

        private ReceiptDTO receipt;
        private ShopDTO shop;

        //Properties
        public string Filter { get; set; }
        public ReceiptDTO Receipt
        {
            get { return receipt; }
            set { SetProperty(ref receipt, value); }
        }
        public ShopDTO Shop
        {
            get { return shop; }
            set { SetProperty(ref shop, value); }

        }
        public List<ProductDTO> ProductsList
        {
            get
            {
                var productsList = service.GetFilteredProductsNotOnList(receipt, Filter).ToList();

                noProductsFound = productsList != null && productsList.Count == 0;
                OnPropertyChanged("NoProductsFound");
                return productsList;
            }
        }

        public IEnumerable<ProductFromListInShopDTO> WantedProducts
        {
            get
            {
                return service.GetProductsFromListInShop(receipt, selectedShop);
            }
        }

        public ShopDTO SelectedShop
        {
            get { return selectedShop; }
            set
            {
                SetProperty(ref selectedShop, value);
                OnPropertyChanged("WantedProducts");
            }
        }

        public NavigationViewModel OwnerWindow
        {
            get { return ownerWindow; }
        }

        public String AcceptDialogText
        {
            get { return acceptDialogText; }
        }

        public bool NoProductsFound
        {
            get { return noProductsFound; }
        }

        //Constructors
        private void initialize(ReceiptDTO receipt, String acceptDialogText)
        {
            this.receipt = receipt;
            shopService = new ShopService();
            this.receipt = receipt;
            FilterProductsCommand = new BaseCommand(FilterProducts);
            OpenAddProductToListDialogCommand = new BaseCommand(OpenAddProductToListDialog);
            OpenEditProductQuantityOnListDialogCommand = new BaseCommand(OpenEditProductQuantityOnListDialog);
            OpenDeleteProductFromListDialogCommand = new BaseCommand(OpenDeleteProductFromListDialog);
            ReceiptInShopsCommand = new BaseCommand(OpenReceiptInShopsWindow);
            this.acceptDialogText = acceptDialogText;
            SelectedShop = new ShopDTO { Id = -1, Name = "None" };
        }

        //for new receipts
        public ReceiptViewModel(String acceptDialogText = "OK")
        {
            service = new ReceiptService();
            initialize(new ReceiptDTO(service.GetDefaultReceipt()), acceptDialogText);
        }

        //for editing of receipts
        public ReceiptViewModel(ReceiptDTO receipt, NavigationViewModel owner, String acceptDialogText = "OK")
        {
            service = new ReceiptService();
            initialize(new ReceiptDTO(receipt), acceptDialogText);
            ownerWindow = owner;
        }

        //functions called by dialogs in receiptsList
        public bool AddReceipt()
        {
            return service.AddReceipt(receipt);
        }

        public bool EditReceiptName()
        {
            return service.UpdateReceipt(receipt);
        }

        public bool DeleteReceipt()
        {
            return service.DeleteReceipt(receipt);
        }

        //Functions related with commands
        public void FilterProducts(object obj)
        {
            Filter = obj as String;
            OnPropertyChanged("ProductsList");
        }

        private async void OpenAddProductToListDialog(object obj)
        {
            addProductToListVM = new ReceiptElementViewModel(Receipt, obj as ProductDTO);
            var dialog = new View.AddProductToListDialog() { DataContext = addProductToListVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingAddProductToListEventHandler);
        }

        private async void OpenEditProductQuantityOnListDialog(object obj)
        {
            editProductQuantityOnListVM = new ReceiptElementViewModel((obj as ProductFromListInShopDTO).ReceiptProduct);
            var dialog = new View.AddProductToListDialog() { DataContext = editProductQuantityOnListVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingEditProductQuantityOnListEventHandler);
        }

        private async void OpenDeleteProductFromListDialog(object obj)
        {
            deleteProductFromListVM = new ReceiptElementViewModel((obj as ProductFromListInShopDTO).ReceiptProduct);

            string dialogMessage = "Are you sure you want to delete this product from your list?";
            var dialog = new DeleteDialog() { DataContext = dialogMessage };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingDeleteProductFromListEventHandler);
        }

        //Closing dialogs handlers
        private void ClosingAddProductToListEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            addProductToListVM.AddProductToList();
            OnPropertyChanged("WantedProducts");
            OnPropertyChanged("ProductsList");
            OnPropertyChanged("Receipt"); //dla TotalPrice
        }

        private void ClosingEditProductQuantityOnListEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            editProductQuantityOnListVM.EditQuantity();
            OnPropertyChanged("WantedProducts");
            OnPropertyChanged("Receipt");
        }

        private void ClosingDeleteProductFromListEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            deleteProductFromListVM.DeleteProductFromList();
            OnPropertyChanged("WantedProducts");
            OnPropertyChanged("ProductsList");
            OnPropertyChanged("Receipt");
        }

        private void OpenReceiptInShopsWindow(object obj)
        {
            var window = new ShopsMapWindow(service.GetReceiptInShops(receipt));
            window.ShowDialog();
            var newSelectedShop = shopService.Get(window.SelectedShop.Id);
            if (newSelectedShop != null)
                SelectedShop = newSelectedShop;
        }
    }
}
