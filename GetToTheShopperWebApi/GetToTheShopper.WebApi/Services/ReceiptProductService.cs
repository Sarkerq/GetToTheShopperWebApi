using GetToTheShopper.WebApi.Exceptions;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Services
{
    public class ReceiptProductService
    {
        GetToTheShopperContext context;
        public ReceiptProductService(GetToTheShopperContext context)
        {
            this.context = context;
        }

        public IEnumerable<ReceiptProduct> GetReceiptProductList(Receipt receipt)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.ReceiptProduct.Include(ReceiptProduct => ReceiptProduct.Product).Where(ReceiptProduct => ReceiptProduct.ReceiptId == receipt.Id).ToList();
            }
        }

        public void AddProductToList(ReceiptProduct receiptsProduct)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.ReceiptProduct.Add(receiptsProduct);
                unitOfWork.Save();
            }
        }

        public void UpdateProductOnList(ReceiptProduct receiptsProduct)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.ReceiptProduct.UpdateByObject(receiptsProduct);
                unitOfWork.Save();
            }
        }

        public void DeleteProductFromList(ReceiptProduct receiptsProduct)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.ReceiptProduct.Remove(receiptsProduct);
                unitOfWork.Save();
            }
        }
        public ReceiptProduct GetReceiptProductById(int id)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.ReceiptProduct.Find(id);
            }
        }
    }
}
