using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO
{
    public class SharedReceiptDTO
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public string UserId { get; set; }
    }
}
