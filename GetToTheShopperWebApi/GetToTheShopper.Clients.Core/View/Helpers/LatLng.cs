using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.View.Helpers
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class LatitudeLongitude
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatitudeLongitude()
        {
            Latitude = double.NaN;
            Longitude = double.NaN;
        }

        public void MarkerChanged(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
    }
}
