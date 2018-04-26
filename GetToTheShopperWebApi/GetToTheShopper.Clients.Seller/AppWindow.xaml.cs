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
using System.Windows.Shapes;

namespace GetToTheShopper.Clients.Seller
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow()
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
