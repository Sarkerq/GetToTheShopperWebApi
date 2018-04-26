using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Client.Helpers
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class SelectedShop
    {
        public int Id { get; set; }

        public SelectedShop()
        {
            Id = -1;
        }

        public void SelectShop(int id)
        {
            Id = id;
        }
    }
}
