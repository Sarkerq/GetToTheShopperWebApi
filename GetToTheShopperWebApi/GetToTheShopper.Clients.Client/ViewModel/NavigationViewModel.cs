using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GetToTheShopper.Clients.Client.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private ViewModelBase selectedViewModel;

        //Properties
        public ICommand ReceiptsListCommand { get; set; }
        public ICommand OpenReceiptCommand { get; set; }
        public ICommand OpenSignUpPageCommand { get; set; }
        public ICommand OpenClientStartCommand { get; set; }

        public ViewModelBase SelectedViewModel
        {
            get { return selectedViewModel; }
            set { SetProperty(ref selectedViewModel, value); }
        }

        //Constructors
        public NavigationViewModel()
        {
            ReceiptsListCommand = new BaseCommand(OpenReceiptsList);
            OpenReceiptCommand = new BaseCommand(OpenReceipt);
            OpenSignUpPageCommand = new BaseCommand(OpenSignUpPage);
            OpenClientStartCommand = new BaseCommand(OpenClientStart);

            SelectedViewModel = new ClientStartViewModel(this);
        }

        private void OpenSignUpPage(object obj)
        {
            SelectedViewModel = new SignUpViewModel(this);

        }
        private void OpenClientStart(object obj)
        {
            SelectedViewModel = new ClientStartViewModel(this);

        }
        private void OpenReceiptsList(object obj)
        {
            SelectedViewModel = new ReceiptsListViewModel(this);
        }

        private void OpenReceipt(object obj)
        {
            if(obj is ReceiptDTO)
                SelectedViewModel = new ReceiptViewModel(obj as ReceiptDTO, this);
        }
    }
}
