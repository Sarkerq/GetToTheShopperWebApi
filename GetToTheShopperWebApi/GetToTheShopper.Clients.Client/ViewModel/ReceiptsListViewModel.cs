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
using GetToTheShopper.Clients.Core.Commands;

namespace GetToTheShopper.Clients.Client.ViewModel
{
    public class ReceiptsListViewModel : ViewModelBase
    {
        private List<ReceiptDTO> receiptsList;

        private int selectedReceiptIndex = -1;

        //Viewmodels related with Dialogs
        private ReceiptViewModel addReceiptNameVM;
        private ReceiptViewModel editReceiptNameVM;
        private ReceiptViewModel deleteReceiptNameVM;

        //Commands
        public ICommand AddReceiptNameDialogCommand { get; set; }
        public ICommand EditReceiptNameDialogCommand { get; set; }
        public ICommand DeleteReceiptNameDialogCommand { get; set; }
        public ICommand MarkAsDoneCommand { get; set; }

        private NavigationViewModel ownerWindow;

        private ReceiptService service;

        //Properties
        public NavigationViewModel OwnerWindow
        {
            get { return ownerWindow; }
        }

        public List<ReceiptDTO> ReceiptsList
        {
            get { return receiptsList; }
            set { SetProperty(ref receiptsList, value); }
        }

        public int SelectedReceiptIndex
        {
            get { return selectedReceiptIndex; }
            set { SetProperty(ref selectedReceiptIndex, value); }
        }

        public ReceiptDTO SelectedReceipt
        {
            get { return ReceiptsList[selectedReceiptIndex]; }
            set { ReceiptsList[selectedReceiptIndex] = value; }
        }
        
        public ReceiptsListViewModel(NavigationViewModel owner)
        {
            service = new ReceiptService();

            ownerWindow = owner;
            AddReceiptNameDialogCommand = new BaseCommand(OpenAddReceiptNameDialog);
            EditReceiptNameDialogCommand = new BaseCommand(OpenEditReceiptNameDialog);
            DeleteReceiptNameDialogCommand = new BaseCommand(OpenDeleteReceiptNameDialog);
            MarkAsDoneCommand = new BaseCommand(MarkReceiptAsDone);
            
            ReceiptsList = service.GetAllReceipts().ToList();
        }
        
        //Functions related with commands
        private void MarkReceiptAsDone(object obj)
        {
            service.ChangeDoneState(SelectedReceipt);
            OnPropertyChanged("receiptsList");
            ReceiptsList = service.GetAllReceipts().ToList();
        }

        private async void OpenAddReceiptNameDialog(object obj)
        {
            addReceiptNameVM = new ReceiptViewModel("Add");
            var dialog = new View.EditReceiptNameDialog() { DataContext = addReceiptNameVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingAddReceiptEventHandler);
        }

        private async void OpenEditReceiptNameDialog(object obj)
        {
            editReceiptNameVM = new ReceiptViewModel(SelectedReceipt, ownerWindow, "Save");
            var dialog = new View.EditReceiptNameDialog() { DataContext = editReceiptNameVM };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingEditReceiptEventHandler);
        }
        
        private async void OpenDeleteReceiptNameDialog(object obj)
        {
            deleteReceiptNameVM = new ReceiptViewModel(SelectedReceipt, ownerWindow);
            string dialogMessage = "Are you sure you want to delete this list?";
            var dialog = new Core.View.DeleteDialog() { DataContext = dialogMessage };

            var result = await DialogHost.Show(dialog, "RootDialog", ClosingDeleteReceiptEventHandler);
        }

        //Closing dialogs handlers
        private void ClosingAddReceiptEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;
            
            addReceiptNameVM.AddReceipt();
            ReceiptsList = service.GetAllReceipts().ToList();
        }
        private void ClosingEditReceiptEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;
            
            editReceiptNameVM.EditReceiptName();
            ReceiptsList = service.GetAllReceipts().ToList();
        }
        private void ClosingDeleteReceiptEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            deleteReceiptNameVM.DeleteReceipt();
            ReceiptsList = service.GetAllReceipts().ToList();
        }
    }
}
