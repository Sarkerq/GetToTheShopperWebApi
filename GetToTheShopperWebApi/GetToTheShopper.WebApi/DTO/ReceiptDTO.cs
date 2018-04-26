using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetToTheShopper.WebApi.DTO
{
    public class ReceiptDTO
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        [Required]
        public string Name { get; set; }

        public bool Done { get; set; }
    }
}
