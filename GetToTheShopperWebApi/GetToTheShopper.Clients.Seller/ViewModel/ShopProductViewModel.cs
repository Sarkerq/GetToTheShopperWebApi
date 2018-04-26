using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GetToTheShopper.Clients.Core.ViewModel;
using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.DTO;
using MaterialDesignThemes.Wpf;
using GetToTheShopper.Clients.Seller.View;

namespace GetToTheShopper.Clients.Seller.ViewModel
{
    public class ShopProductViewModel : ViewModelBase
    {
        private String acceptDialogText;

        private ShopProductService shopProductService;
        private ProductService productService;
        private ShopService shopService;
        private bool noProductsFound;
        public ICommand FilterProductsCommand { get; set; }
        public ICommand ChooseProductCommand { get; set; }

        public bool NoProductsFound
        {
            get
            {
                return noProductsFound;
            }
            set
            {
                SetProperty(ref noProductsFound, value);
            }
        }
        private ShopProductDTO shopProduct;
        public string Filter { get; set; }

        //Properties
        public ShopProductDTO ShopProduct
        {
            get { return shopProduct; }
            set { shopProduct = value; }
        }
        public IEnumerable<ProductDTO> ProductsList
        {
            get
            {
                var productsList = shopService.GetFilteredProductsNotInShop(shopService.Get(shopProduct.ShopId), Filter).ToList();

                noProductsFound = productsList != null && productsList.Count == 0;
                OnPropertyChanged("NoProductsFound");
                return productsList;
            }
        }
        public IEnumerable<Unit> UnitValues
        {
            get { return Unit.UnitValues; }
        }

        public ProductDTO Product
        {
            get { return ShopProduct.Product; }
            set { ShopProduct.Product = value; }
        }

        public String AcceptDialogText
        {
            get { return acceptDialogText; }
            set { SetProperty(ref acceptDialogText, value); }
        }
        private void ChooseProduct(object obj)
        {
            ShopProduct.Product = obj as ProductDTO;
            OnPropertyChanged("ShopProduct");
            OnPropertyChanged("IsProductNull");
        }
        private void ClosingAddProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            AddShopProduct();
        }
        public Unit SelectedUnit
        {
            get { return Product.Unit; }
            set { Product.Unit = value; }
        }

        public bool IsProductNull
        {
            get { return ShopProduct.Product == null; }
        }

        void initialize(String acceptDialogText)
        {
            ChooseProductCommand = new BaseCommand(ChooseProduct);
            shopProductService = new ShopProductService();
            productService = new ProductService();
            shopService = new ShopService();
            FilterProductsCommand = new BaseCommand(FilterProducts);
            AcceptDialogText = acceptDialogText;

        }
        //Constructors

        //for adding
        public ShopProductViewModel(ShopDTO shop, String acceptDialogText = "OK")
        {
            initialize(acceptDialogText);

            shopProduct = new ShopProductDTO();
            shopProduct.ShopId = shop.Id;
            ShopProduct.Product = null;
        }
        //for editing
        public ShopProductViewModel(ShopProductDTO shopProduct, String acceptDialogText = "OK")
        {
            initialize(acceptDialogText);

            shopProductService = new ShopProductService();
            ShopProduct = new ShopProductDTO(shopProduct);
        }
        public void FilterProducts(object obj)
        {
            Filter = obj as String;
            OnPropertyChanged("ProductsList");
        }

        //functions called by dialogs in productslist
        public bool AddShopProduct()
        {
            return shopProductService.AddShopProduct(shopProduct);
        }

        public bool EditShopProduct()
        {
            return shopProductService.EditShopProduct(shopProduct);

        }

        public bool DeleteShopProduct()
        {
            return shopProductService.DeleteShopProduct(shopProduct);
        }
    }
}
