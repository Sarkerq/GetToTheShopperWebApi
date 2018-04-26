using ShoppingList.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.ModelHelper
{
    public class ProductFromListInShop
    {
        public ShopProduct ShopProduct { get; set; }
        public Product Product { get; set; }
        public double WantedQuantity { get; set; }
        public bool? EnoughUnits { get; set; }
        public ReceiptProduct ReceiptProduct { get; set; }
    }
}
