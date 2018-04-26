using GetToTheShopper.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO.Assemblers
{
    public class ReceiptAssembler
    {
        public ReceiptDTO GetDTO(Receipt receipt)
        {
            return new ReceiptDTO()
            {
                Id = receipt.Id,
                AuthorId = receipt.AuthorId,
                Name = receipt.Name,
                Done = receipt.Done
            };
        }
        public Receipt GetModel(ReceiptDTO receiptDTO)
        {
            return new Receipt
            {
                Id = receiptDTO.Id,
                AuthorId = receiptDTO.AuthorId,
                Done = receiptDTO.Done,
                Name = receiptDTO.Name
            };
        }
    }
}
