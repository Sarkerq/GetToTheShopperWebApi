using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace GetToTheShopper.WebApi.Models
{
    public class SharedReceipt
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public string UserId { get; set; }

        public virtual Receipt Receipt { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
