using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetToTheShopper.WebApi.DTO
{
    public class ReceiptProductDTO
    {
        public int Id { get; set; }
        [Required]
        public int ReceiptId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public double Quantity { get; set; }
    }
}
