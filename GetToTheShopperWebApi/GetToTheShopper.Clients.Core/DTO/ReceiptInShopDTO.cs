using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.DTO
{
    public class ReceiptInShopDTO
    {
        public ShopDTO Shop { get; set; }
        public double Availability { get; set; }
    }
}
