using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Model
{
    public class Receipt : ModelBase
    {
        private bool done;
        private string name;

        public int Id { get; set; }
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        public int AuthorId { get; set; }

        public bool Done
        {
            get { return done; }
            set { SetProperty(ref done, value); }
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
