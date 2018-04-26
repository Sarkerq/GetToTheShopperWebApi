using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO.Assemblers
{
    public class SharedReceiptAssembler
    {

        public SharedReceiptAssembler()
        {
        }

        public SharedReceiptDTO GetDTO(SharedReceipt SharedReceipt)
        {
            return new SharedReceiptDTO()
            {
                UserId = SharedReceipt.UserId,
                ReceiptId = SharedReceipt.ReceiptId,
                Id = SharedReceipt.Id
            };

        }
        public SharedReceipt GetModel(SharedReceiptDTO SharedReceiptDTO)
        {
            return new SharedReceipt
            {
                Id = SharedReceiptDTO.Id,
                UserId = SharedReceiptDTO.UserId,
                ReceiptId = SharedReceiptDTO.ReceiptId,
            };
        }
    }
}
