using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.ViewModel;
using GetToTheShopper.Clients.Seller.View;
using GetToTheShopper.Clients.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GetToTheShopper.Clients.Core.View.Helpers;
using MaterialDesignThemes.Wpf;
using GetToTheShopper.Clients.Core.View;

namespace GetToTheShopper.Clients.Seller.ViewModel
{
    class ShopViewModel : ViewModelBase
    {
        private String acceptDialogText;
        private ShopService service;
        private ShopDTO shop;
        private bool isNew;
        public NavigationViewModel OwnerWindow { get; set; }
        //Properties
        public ICommand SelectFromMapCommand { get; set; }
        private IEnumerable<ShopProductDTO> _ShopProductList;
        private ShopProductViewModel addProductVM;
        private ShopProductViewModel editShopProductVM;
        private ShopProductViewModel deleteShopProductVM;
        private ShopProductService shopProductService;
        private ProductService productService;

        public ICommand SelectProductDialogCommand { get; set; }
        public ICommand EditShopProductDialogCommand { get; set; }
        public ICommand DeleteShopProductDialogCommand { get; set; }


        public IEnumerable<ShopProductDTO> ShopProductList
        {
            get
            {
                if (_ShopProductList == null) return null;
                return _ShopProductList.OrderBy(p => p.Product.Name);
            }
            set
            {
                SetProperty(ref _ShopProductList, value);
            }
        }

        //Constructors


        private async void OpenSelectProductDialog(object obj)
        {
            addProductVM = new ShopProductViewModel(shop, "Add");
            var dialog = new View.EditShopProductDialog() { DataContext = addProductVM };
            var result = await DialogHost.Show(dialog, "RootDialog", ClosingAddProductEventHandler).ConfigureAwait(false);
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
            ShopProductList = shopProductService.GetShopProductListByShopId(Shop.Id);
        }
        private void ClosingEditProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            editShopProductVM.EditShopProduct();
            ShopProductList = shopProductService.GetShopProductListByShopId(Shop.Id);
        }
        private void ClosingDeleteProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            deleteShopProductVM.DeleteShopProduct();
            ShopProductList = shopProductService.GetShopProductListByShopId(Shop.Id);
        }
        public ShopDTO Shop
        {
            get { return shop; }
            set { SetProperty(ref shop, value); }
        }
        public String AcceptDialogText
        {
            get { return acceptDialogText; }
            set { SetProperty(ref acceptDialogText, value); }
        }

        //Constructors
        private void initialize(String acceptDialogText)
        {
            service = new ShopService();
            productService = new ProductService();
            shopProductService = new ShopProductService();
            AcceptDialogText = acceptDialogText;
            SelectFromMapCommand = new BaseCommand(SelectFromMap);

        }
        //for adding
        public ShopViewModel(String acceptDialogText = "OK")
        {
            initialize(acceptDialogText);
            Shop = service.GetDefaultShop();

            isNew = true;
        }
        //for editing
        public ShopViewModel(ShopDTO shop, NavigationViewModel ownerWindow, String acceptDialogText = "OK")
        {

            initialize(acceptDialogText);
            Shop = new ShopDTO(shop);
            ShopProductList = shopProductService.GetShopProductListByShopId(Shop.Id);

            isNew = false;
            OwnerWindow = ownerWindow;
            SelectProductDialogCommand = new BaseCommand(OpenSelectProductDialog);
            EditShopProductDialogCommand = new BaseCommand(OpenEditShopProductDialog);
            DeleteShopProductDialogCommand = new BaseCommand(OpenDeleteShopProductDialog);

        }


        private void SelectFromMap(object obj)
        {
            LatitudeLongitude latLongitude = new LatitudeLongitude();
            if (!isNew)
            {
                latLongitude.Latitude = Shop.Latitude;
                latLongitude.Longitude = Shop.Longitude;
            }
            var window = new ShopMapWindow(latLongitude);
            window.ShowDialog();
            Shop.Latitude = window.LatitudeLongitude.Latitude;
            Shop.Longitude = window.LatitudeLongitude.Longitude;
        }

        public bool AddShop()
        {
            return service.AddShop(shop);
        }
        public bool EditShop()
        {
            return service.EditShop(shop);
        }
        public bool DeleteShop()
        {
            return service.DeleteShop(shop);
        }
    }
}
