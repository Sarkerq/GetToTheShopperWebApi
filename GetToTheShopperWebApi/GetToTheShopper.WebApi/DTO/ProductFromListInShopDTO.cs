using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO
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
