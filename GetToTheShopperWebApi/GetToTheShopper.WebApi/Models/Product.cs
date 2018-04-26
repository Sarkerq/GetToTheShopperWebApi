using GetToTheShopper.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Models
{

    public class Product
    {
        public int Id { get; set; }
        public string Name
        {
            get;
            set;
        }
        public Unit.UnitName Unit
        {
            get;
            set;
        }

        public virtual ICollection<ShopProduct> ShopProducts { get; set; }
        public virtual ICollection<ReceiptProduct> ReceiptProduct { get; set; }
        public double Price
        {
            get;
            set;
        }
        public Product()
        {
            Unit = DTO.Unit.UnitName.sztuka;
        }

        public Product(Product pattern)
        {
            Id = pattern.Id;
            Name = pattern.Name;
            Unit = pattern.Unit;
            Price = pattern.Price;
        }
    }

}
