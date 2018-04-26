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

namespace GetToTheShopper.Clients.Seller.View
{
    /// <summary>
    /// Interaction logic for AddShopMapHelper.xaml
    /// </summary>
    public partial class ShopMapWindow : Window
    {
        public LatitudeLongitude LatitudeLongitude { get; set; } = new LatitudeLongitude();

        public ShopMapWindow(LatitudeLongitude latLongitude)
        {
            LatitudeLongitude = latLongitude;
            InitializeComponent();
            Map.Source = new Uri(new System.IO.FileInfo("View\\AddShopMap.html").FullName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Map_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ((WebBrowser)sender).ObjectForScripting = LatitudeLongitude;
            if(!Double.IsNaN(LatitudeLongitude.Latitude) && !Double.IsNaN(LatitudeLongitude.Longitude))
            {
                Map.InvokeScript("addMarker", new Object[] { LatitudeLongitude.Latitude, LatitudeLongitude.Longitude });
            }
        }
    }
}
