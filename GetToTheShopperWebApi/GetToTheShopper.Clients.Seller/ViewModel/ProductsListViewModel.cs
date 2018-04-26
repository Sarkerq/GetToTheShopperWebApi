using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.View;
using GetToTheShopper.Clients.Core.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GetToTheShopper.Clients.Seller.ViewModel
{
    class ProductsListViewModel : ViewModelBase
    {
        private IEnumerable<ProductDTO> _ProductList;
        private ProductViewModel addProductVM;
        private ProductViewModel editProductVM;
        private ProductViewModel deleteProductVM;
        private ProductService service;

        public NavigationViewModel OwnerWindow { get; set; }

        //Properties
        public ICommand AddProductDialogCommand { get; set; }
        public ICommand EditProductDialogCommand { get; set; }
        public ICommand DeleteProductDialogCommand { get; set; }


        public IEnumerable<ProductDTO> ProductList
        {
            get
            {
                return _ProductList.OrderBy(p => p.Name);
            }
            set
            {
                SetProperty(ref _ProductList, value);
            }
        }

        //Constructors
        public ProductsListViewModel(NavigationViewModel ownerWindow)
        {
            service = new ProductService();
            OwnerWindow = ownerWindow;

            AddProductDialogCommand = new BaseCommand(OpenAddProductDialog);
            EditProductDialogCommand = new BaseCommand(OpenEditProductDialog);
            DeleteProductDialogCommand = new BaseCommand(OpenDeleteProductDialog);

            ProductList = service.GetProductList();
        }

        private async void OpenAddProductDialog(object obj)
        {
            addProductVM = new ProductViewModel("Add");
            var dialog = new View.EditProductDialog() { DataContext = addProductVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingAddProductEventHandler).ConfigureAwait(false);
        }
        private async void OpenEditProductDialog(object obj)
        {
            editProductVM = new ProductViewModel(obj as ProductDTO, "Edit");
            var dialog = new View.EditProductDialog() { DataContext = editProductVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingEditProductEventHandler).ConfigureAwait(false);
        }
        private async void OpenDeleteProductDialog(object obj)
        {
            deleteProductVM = new ProductViewModel(obj as ProductDTO);
            string dialogMessage = "Are you sure you want to delete this product?";
            var dialog = new DeleteDialog() { DataContext = dialogMessage };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingDeleteProductEventHandler).ConfigureAwait(false);
        }
        private void ClosingAddProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            addProductVM.AddProduct();
            ProductList = service.GetProductList();
        }
        private void ClosingEditProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            editProductVM.EditProduct();
            ProductList = service.GetProductList();
        }
        private void ClosingDeleteProductEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            deleteProductVM.DeleteProduct();
            ProductList = service.GetProductList();
        }
    }
}
