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
    public class ReceiptProduct
    {
        private double quantity;

        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public virtual Receipt Receipt { get; set; }
        public virtual Product Product { get; set; }
    }
}
