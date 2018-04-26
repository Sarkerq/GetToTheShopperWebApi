using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GetToTheShopper.Clients.Seller.ViewModel
{
    class SelectCategoryViewModel : ViewModelBase
    {
        private NavigationViewModel ownerWindow;
        private bool progress;

        //Properties
        public ICommand OpenProductsListCommand { get; set; }
        public ICommand OpenShopsListCommand { get; set; }

        public bool Progress
        {
            get { return progress; }
            set
            { SetProperty(ref progress, value); }
        }

        public NavigationViewModel OwnerWindow
        {
            get { return ownerWindow; }
            set { SetProperty(ref ownerWindow, value); }
        }

        //Constructors
        public SelectCategoryViewModel(NavigationViewModel ownerWindow)
        {
            OpenProductsListCommand = new BaseCommand(OpenProductsList);
            OpenShopsListCommand = new BaseCommand(OpenShopsList);
            OwnerWindow = ownerWindow;
        }

        private void OpenProductsList(object obj)
        {
            Progress = true;
            new Task(() => OwnerWindow.ProductsListCommand.Execute(null)).Start();
        }

        private void OpenShopsList(object obj)
        {
            Progress = true;
            new Task(() => OwnerWindow.ShopsListCommand.Execute(null)).Start();
        }
    }
}
