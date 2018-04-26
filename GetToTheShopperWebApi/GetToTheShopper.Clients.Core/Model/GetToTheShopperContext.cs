using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Model
{
    public class GetToTheShopperContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptProduct> ReceiptProducts { get; set; }
        public DbSet<ShopProduct> ShopProducts { get; set; }
    }
}
