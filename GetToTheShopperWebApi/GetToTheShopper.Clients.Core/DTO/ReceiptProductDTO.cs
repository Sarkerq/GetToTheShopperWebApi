using System;
using System.Collections.Generic;
using System.Text;

namespace GetToTheShopper.Clients.Core.DTO
{
    public class ReceiptProductDTO
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
