using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.DTO
{
    public class ProductFromListInShopDTO
    {
        public ShopProductDTO ShopProduct { get; set; }
        public ProductDTO Product { get; set; }
        public double WantedQuantity { get; set; }
        public bool? EnoughUnits { get; set; }
        public ReceiptProductDTO ReceiptProduct { get; set; }
    }
}
