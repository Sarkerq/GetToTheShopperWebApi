using GetToTheShopper.Clients.Core.Commands;
using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.Services;
using GetToTheShopper.Clients.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GetToTheShopper.Clients.Client.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        private NavigationViewModel ownerWindow;
        public ICommand SignUpCommand { get; set; }
        private bool progress;
        SignUpService service;
        private bool signUpFailed = false;
        public bool SignUpFailed { get => signUpFailed; set => SetProperty(ref signUpFailed, value); }
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
        public RegisterDTO RegistrationData { get; set; }
        public SignUpViewModel(NavigationViewModel owner)
        {
            ownerWindow = owner;
            SignUpCommand = new BaseCommand(SignUpAndBackToStartPage);
            RegistrationData = new RegisterDTO { UserRoles = "Client" };
            service = new SignUpService();
        }

        private void SignUpAndBackToStartPage(object obj)
        {
            Progress = true;
            if (service.SignUp(RegistrationData))
            {
                new Task(() => OwnerWindow.OpenClientStartCommand.Execute(null)).Start();
            }
            else
            {
                Progress = false;
                SignUpFailed = true;
            }
        }
    }
}
