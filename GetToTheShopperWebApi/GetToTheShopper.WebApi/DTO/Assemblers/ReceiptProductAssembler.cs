using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO.Assemblers
{
    public class ReceiptProductAssembler
    {

        public ReceiptProductAssembler()
        {
        }

        public ReceiptProductDTO GetDTO(ReceiptProduct receiptProduct)
        {
            return new ReceiptProductDTO()
            {
                Quantity = receiptProduct.Quantity,
                ProductId = receiptProduct.ProductId,
                ReceiptId = receiptProduct.ReceiptId,
                Id = receiptProduct.Id
            };

        }
        public ReceiptProduct GetModel(ReceiptProductDTO receiptProductDTO)
        {
            return new ReceiptProduct
            {
                Id = receiptProductDTO.Id,
                ProductId = receiptProductDTO.ProductId,
                ReceiptId = receiptProductDTO.ReceiptId,
                Quantity = receiptProductDTO.Quantity
            };
        }
    }
}
