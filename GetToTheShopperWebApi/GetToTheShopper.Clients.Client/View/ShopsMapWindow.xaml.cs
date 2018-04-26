using GetToTheShopper.Clients.Client.Helpers;
using GetToTheShopper.Clients.Core.DTO;
using GetToTheShopper.Clients.Core.View.Helpers;
using System;
using System.Collections.Generic;
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

namespace GetToTheShopper.Clients.Client.View
{
    /// <summary>
    /// Interaction logic for AddShopMapHelper.xaml
    /// </summary>
    public partial class ShopsMapWindow : Window
    {
        IEnumerable<ReceiptInShopDTO> shopsList;
        public SelectedShop SelectedShop { get; set; }

        public ShopsMapWindow(IEnumerable<ReceiptInShopDTO> shopsList)
        {
            this.shopsList = shopsList;
            SelectedShop = new SelectedShop();
            InitializeComponent();
            Map.Source = new Uri(new System.IO.FileInfo("View\\ShopsMap.html").FullName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Map_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ((WebBrowser)sender).ObjectForScripting = SelectedShop;
            foreach(var shop in shopsList)
            {
                Map.InvokeScript("addMarker", new Object[] { shop.Shop.Id, shop.Shop.Latitude, shop.Shop.Longitude, shop.Shop.Name, shop.Shop.Address, shop.Availability });
            }
        }
    }
}
