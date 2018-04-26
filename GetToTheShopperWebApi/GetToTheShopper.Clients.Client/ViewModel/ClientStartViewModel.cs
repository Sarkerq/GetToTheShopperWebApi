using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GetToTheShopper.Clients.Client.ViewModel
{
    class ClientStartViewModel : ViewModelBase
    {
        private bool progress;
        private NavigationViewModel ownerWindow;
        public LoginDTO LoginData { get; set; }
        //Properties
        public ICommand OpenReceiptsListCommand { get; set; }
        public ICommand OpenSignUpPageCommand { get; set; }
        public ICommand LogInCommand { get; set; }
        private bool logiInFailed = false;
        public bool LogInFailed { get => logiInFailed; set => SetProperty(ref logiInFailed, value); }
        LogInService service;
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



        //constructor
        public ClientStartViewModel(NavigationViewModel ownerWindow)
        {
            OwnerWindow = ownerWindow;

            OpenReceiptsListCommand = new BaseCommand(OpenReceiptsList);
            OpenSignUpPageCommand = new BaseCommand(OpenSignUpPage);
            LogInCommand = new BaseCommand(LogIn);
            LoginData = new LoginDTO();
            service = new LogInService();
        }

        private void OpenReceiptsList(object obj)
        {
            Progress = true;
            new Task(() => OwnerWindow.ReceiptsListCommand.Execute(null)).Start();
        }
        private void OpenSignUpPage(object obj)
        {
            Progress = true;
            new Task(() => OwnerWindow.OpenSignUpPageCommand.Execute(null)).Start();
        }


        private void LogIn(object obj)
        {
            Progress = true;
            if (service.LogIn(LoginData))
            {
                OpenReceiptsList(obj);
            }
            else
            {
                LogInFailed = true;
                Progress = false;
            }
        }
    }
}
