using GetToTheShopper.Clients.Core.ViewModel;
using GetToTheShopper.Clients.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.Services;

namespace GetToTheShopper.Clients.Seller.ViewModel
{
    class ProductViewModel : ViewModelBase
    {
        private String acceptDialogText;

        private ProductService service;

        private ProductDTO product;

        //Properties
        public ProductDTO Product
        {
            get { return product; }
            set { product = value; }
        }

        public IEnumerable<Unit> UnitValues
        {
            get { return Unit.UnitValues; }
        }



        public String AcceptDialogText
        {
            get { return acceptDialogText; }
            set { SetProperty(ref acceptDialogText, value); }
        }

        public Unit SelectedUnit
        {
            get { return Product.Unit; }
            set
            {
                Product.Unit = value;
                OnPropertyChanged();
            }
        }

        //Constructors
        //for adding
        public ProductViewModel(String acceptDialogText = "OK")
        {
            service = new ProductService();
            product = service.GetDefaultProduct();
            AcceptDialogText = acceptDialogText;
        }
        //for editing
        public ProductViewModel(ProductDTO product, String acceptDialogText = "OK")
        {
            service = new ProductService();
            Product = new ProductDTO(product);
            AcceptDialogText = acceptDialogText;
        }

        //functions called by dialogs in productslist
        public bool AddProduct()
        {
            return service.AddProduct(product);
        }

        public bool EditProduct()
        {
            return service.EditProduct(product);

        }

        public bool DeleteProduct()
        {
            return service.DeleteProduct(product);
        }
    }
}
