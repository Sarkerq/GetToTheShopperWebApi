using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.View;
using GetToTheShopper.Clients.Core.ViewModel;
using GetToTheShopper.Clients.Seller.View;
using GetToTheShopper.Clients.Core.DTO;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GetToTheShopper.Clients.Seller.ViewModel
{
    class ShopsListViewModel : ViewModelBase
    {
        public NavigationViewModel OwnerWindow { get; set; }

        private IEnumerable<ShopDTO> shopsList;
        private ShopViewModel addShopVM;
        private ShopViewModel editShopVM;
        private ShopViewModel deleteShopVM;
        private ShopService service;

        //Properties
        public ICommand AddShopDialogCommand { get; set; }
        public ICommand EditShopDialogCommand { get; set; }
        public ICommand DeleteShopDialogCommand { get; set; }

        public IEnumerable<ShopDTO> ShopsList
        {
            get { return shopsList.OrderBy(s => s.Name); }
            set { SetProperty(ref shopsList, value); }
        }

        //Constructors
        public ShopsListViewModel(NavigationViewModel ownerWindow)
        {
            OwnerWindow = ownerWindow;
            service = new ShopService();

            AddShopDialogCommand = new BaseCommand(OpenAddShopDialog);
            EditShopDialogCommand = new BaseCommand(OpenEditShopDialog);
            DeleteShopDialogCommand = new BaseCommand(OpenDeleteShopDialog);

            ShopsList = service.GetShopsList();
        }

        private async void OpenAddShopDialog(object obj)
        {
            addShopVM = new ShopViewModel("Add");
            var dialog = new EditShopDialog() { DataContext = addShopVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingAddShopEventHandler);
        }
        private async void OpenEditShopDialog(object obj)
        {
            editShopVM = new ShopViewModel(obj as ShopDTO, OwnerWindow, "Edit");
            var dialog = new EditShopDialog() { DataContext = editShopVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingEditShopEventHandler);
        }
        private async void OpenDeleteShopDialog(object obj)
        {
            deleteShopVM = new ShopViewModel(obj as ShopDTO, OwnerWindow);
            string dialogMessage = "Are you sure you want to delete this shop?";
            var dialog = new DeleteDialog() { DataContext = dialogMessage };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingDeleteShopEventHandler);
        }

        private void ClosingAddShopEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            addShopVM.AddShop();
            ShopsList = service.GetShopsList();
        }
        private void ClosingEditShopEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            editShopVM.EditShop();
            ShopsList = service.GetShopsList();
        }
        private void ClosingDeleteShopEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            deleteShopVM.DeleteShop();
            ShopsList = service.GetShopsList();
        }
    }
}
