using System;
using System.Collections.Generic;
using System.Text;

namespace GetToTheShopper.Clients.Core.DTO
{
    public class ReceiptDTO
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }

        public ReceiptDTO() { }

        public ReceiptDTO(ReceiptDTO pattern)
        {
            Id = pattern.Id;
            AuthorId = pattern.AuthorId;
            Name = pattern.Name;
            Done = pattern.Done;
        }
    }
}
