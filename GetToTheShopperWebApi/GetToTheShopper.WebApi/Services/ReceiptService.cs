using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.Exceptions;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.ModelsHelpers;
using GetToTheShopper.WebApi.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Services
{
    public class ReceiptService
    {
        GetToTheShopperContext context;
        public ReceiptService(GetToTheShopperContext context)
        {
            this.context = context;
        }

        public void AddReceipt(Receipt receipts)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                if (unitOfWork.Receipts.FirstOrDefault(p => p.Name == receipts.Name) != null)
                    throw new AttributeAlreadyExistsException("Receipt", "name");

                unitOfWork.Receipts.Add(receipts);
                unitOfWork.Save();
            }
        }

        public void UpdateReceipt(Receipt receipts)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {

                unitOfWork.Receipts.UpdateByObject(receipts);
                unitOfWork.Save();
            }
        }

        public void DeleteReceipt(Receipt receipts)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {

                unitOfWork.Receipts.Remove(receipts);
                unitOfWork.Save();
            }
        }

        public IEnumerable<Receipt> GetAllReceipts()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.Receipts.GetAll();
            }
        }
        public IEnumerable<Receipt> GetUsersReceipts(string userId)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return GetAllReceipts().Where(r=> r.AuthorId == userId);
            }
        }

        public IEnumerable<ProductFromListInShop> GetProductsFromListInShop(Receipt receipts, int shopId)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                if (unitOfWork.Receipts.Find(receipts.Id) == null)
                    throw new NonExistingRecordException("Receipt", "id");

                return unitOfWork.Receipts.GetProductsFromListInShop(receipts, shopId);
            }
        }

        public IEnumerable<ReceiptInShopDTO> GetReceiptInShops(Receipt receipt)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                if (unitOfWork.Receipts.Find(receipt.Id) == null)
                    throw new NonExistingRecordException("Receipt", "id");

                return unitOfWork.Receipts.GetReceiptInShops(receipt);
            }
        }



        public IEnumerable<Product> GetFilteredProductsNotOnList(Receipt receipts, string filterString)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                if (unitOfWork.Receipts.Find(receipts.Id) == null)
                    throw new NonExistingRecordException("Receipt", "id");

                return (unitOfWork.Receipts.GetFilteredProductsNotOnList(receipts, filterString));
            }
        }

        internal IEnumerable<ReceiptProduct> GetReceiptProductsByReceiptId(int id)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                if (unitOfWork.Receipts.Find(id) == null)
                    throw new NonExistingRecordException("Receipt", "id");

                return unitOfWork.ReceiptProduct.Where(rp => rp.ReceiptId == id);
            }
        }

        public Receipt GetReceiptById(int receiptId)
        {
            if (receiptId < 0) //default
            {
                return GetDefaultReceipt();
            }
            using (var unitOfWork = new UnitOfWork(context))
            {

                var receipt = unitOfWork.Receipts.Find(receiptId);
                if (receipt == null)
                    throw new NonExistingRecordException("Receipt", "id");
                return receipt;
            }
        }
        
        public Receipt GetDefaultReceipt()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                int index = 1;
                while (unitOfWork.Receipts.Any(r => r.Name == "New Receipt " + index.ToString()))
                    index++;
                return new Receipt() { Name = "New Receipt " + index.ToString() };
            }
        }

        public void ChangeDoneState(Receipt receipts)
        {
            receipts.Done = !receipts.Done;
            UpdateReceipt(receipts);
        }
    }
}
