using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GetToTheShopper.Clients.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var current = Directory.GetCurrentDirectory();
            var parent = current.Substring(0, current.LastIndexOf('\\'));
#if DEBUG
            parent = current.Substring(0, parent.LastIndexOf('\\'));
            parent = current.Substring(0, parent.LastIndexOf('\\'));
#endif
            var dbDirectory = parent + "\\GetToTheShopperDatabase";
            AppDomain.CurrentDomain.SetData("DataDirectory", dbDirectory);
            InitializeComponent();
            DataContext = new ViewModel.NavigationViewModel();
        }
    }
}
