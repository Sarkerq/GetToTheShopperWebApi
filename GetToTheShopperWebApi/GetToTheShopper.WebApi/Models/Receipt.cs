using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Models
{
    public class Receipt
    {
        private bool done;
        private string name;

        public int Id { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string AuthorId { get; set; }

        public bool Done
        {
            get { return done; }
            set { done = value; }
        }

        public virtual ICollection<ReceiptProduct> ReceiptProduct { get; set; }

        public Receipt()
        { }

        public Receipt(Receipt pattern)
        {
            Id = pattern.Id;
            Name = pattern.name;
            AuthorId = pattern.AuthorId;
            Done = pattern.Done;
        }
    }
}
