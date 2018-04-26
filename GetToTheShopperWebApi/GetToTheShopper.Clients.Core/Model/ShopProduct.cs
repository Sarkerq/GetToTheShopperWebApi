using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Model
{
    public class ShopProduct : ModelBase
    {
        private double avaiableUnits;
        private string aisle;

        public int Id { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public double AvailableUnits
        {
            get { return avaiableUnits; }
            set { SetProperty(ref avaiableUnits, value); }
        }
        public string Aisle
        {
            get { return aisle; }
            set { SetProperty(ref aisle, value); }
        }

        public virtual Shop Shop { get; set; }
        public virtual Product Product { get; set; }

        public ShopProduct() { }

        public ShopProduct(ShopProduct pattern)
        {
            Id = pattern.Id;
            ShopId = pattern.ShopId;
            ProductId = pattern.ProductId;
            AvailableUnits = pattern.AvailableUnits;
            Aisle = pattern.Aisle;
            Product = new Product(pattern.Product);
        }
    }
}
