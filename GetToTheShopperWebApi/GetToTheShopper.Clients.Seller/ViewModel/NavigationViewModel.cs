using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GetToTheShopper.Clients.Core.ViewModel;
using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.DTO;

namespace GetToTheShopper.Clients.Seller.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {

        private ViewModelBase selectedViewModel;

        //Properties
        public ICommand ProductsListCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public ICommand ShopsListCommand { get; set; }
        public ICommand SellerStartCommand { get; set; }
        public ICommand SelectCategoryCommand { get; set; }
        public ICommand SignUpPageCommand { get; set; }

        public ViewModelBase SelectedViewModel
        {

            get { return selectedViewModel; }
            set { SetProperty(ref selectedViewModel, value); }
        }

        //Constructors
        public NavigationViewModel()
        {
            ProductsListCommand = new BaseCommand(OpenProductsList);
            ShopCommand = new BaseCommand(OpenShop);
            ShopsListCommand = new BaseCommand(OpenShopsList);
            SellerStartCommand = new BaseCommand(OpenSellerStart);
            SelectCategoryCommand = new BaseCommand(OpenSelectCategory);
            SignUpPageCommand = new BaseCommand(OpenSignUpPage);

            SelectedViewModel = new AdminStartViewModel(this);
        }

        private void OpenSignUpPage(object obj)
        {
            SelectedViewModel = new SignUpViewModel(this);
        }

        private void OpenSelectCategory(object obj)
        {
            SelectedViewModel = new SelectCategoryViewModel(this);
        }

        private void OpenProductsList(object obj)
        {
            SelectedViewModel = new ProductsListViewModel(this);
        }
        private void OpenShop(object obj)
        {
            if (obj is ShopDTO)
                SelectedViewModel = new ShopViewModel(obj as ShopDTO, this);
        }
        private void OpenShopsList(object obj)
        {
            SelectedViewModel = new ShopsListViewModel(this);
        }
        private void OpenSellerStart(object obj)
        {
            SelectedViewModel = new AdminStartViewModel(this);
        }
    }
}
