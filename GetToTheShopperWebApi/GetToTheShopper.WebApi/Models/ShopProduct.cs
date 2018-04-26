using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Models
{
    public class ShopProduct
    {
        private double avaiableUnits;
        private string aisle;

        public int Id { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public double AvailableUnits
        {
            get { return avaiableUnits; }
            set { avaiableUnits = value; }
        }
        public string Aisle
        {
            get { return aisle; }
            set { aisle = value; }
        }

        public virtual Shop Shop { get; set; }
        public virtual Product Product { get; set; }
    }
}
