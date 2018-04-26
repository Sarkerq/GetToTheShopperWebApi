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
using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.View;
using GetToTheShopper.Clients.Core.DTO;

namespace GetToTheShopper.Clients.Seller.ViewModel
{
    class ShopProductsListViewModel : ViewModelBase
    {
        private IEnumerable<ShopProductDTO> _ShopProductList;
        private ShopProductViewModel addProductVM;
        private ShopProductViewModel editShopProductVM;
        private ShopProductViewModel deleteShopProductVM;
        private ShopProductService service;
        //Properties
        public NavigationViewModel OwnerWindow { get; set; }

        public ICommand EditShopProductDialogCommand { get; set; }
        public ICommand DeleteShopProductDialogCommand { get; set; }


        public IEnumerable<ShopProductDTO> ShopProductList
        {
            get
            {
                return _ShopProductList.OrderBy(p => p.Product.Name);
            }
            set
            {
                SetProperty(ref _ShopProductList, value);
            }
        }

        public ShopDTO Shop { get; set; }

        //Constructors
        public ShopProductsListViewModel(ShopDTO shopDTO, NavigationViewModel ownerWindow)
        {
            service = new ShopProductService();
            Shop = shopDTO;
            OwnerWindow = ownerWindow;
            EditShopProductDialogCommand = new BaseCommand(OpenEditShopProductDialog);
            DeleteShopProductDialogCommand = new BaseCommand(OpenDeleteShopProductDialog);

            ShopProductList = service.GetShopProductListByShopId(Shop.Id);
        }
        
        private async void OpenEditShopProductDialog(object obj)
        {
            editShopProductVM = new ShopProductViewModel(obj as ShopProductDTO, "Edit");
            var dialog = new View.EditShopProductDialog() { DataContext = editShopProductVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingEditProductEventHandler).ConfigureAwait(false);
        }
        private async void OpenDeleteShopProductDialog(object obj)
        {
            deleteShopProductVM = new ShopProductViewModel(obj as ShopProductDTO);
            string dialogMessage = "Are you sure you want to delete this shop product?";
            var dialog = new DeleteDialog() { DataContext = dialogMessage };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingDeleteProductEventHandler).ConfigureAwait(false);
        }
        private void ClosingAddProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            addProductVM.AddShopProduct();
            ShopProductList = service.GetShopProductList();
        }
        private void ClosingEditProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            editShopProductVM.EditShopProduct();
            ShopProductList = service.GetShopProductList();
        }
        private void ClosingDeleteProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            deleteShopProductVM.DeleteShopProduct();
            ShopProductList = service.GetShopProductList();
        }
    }
}
