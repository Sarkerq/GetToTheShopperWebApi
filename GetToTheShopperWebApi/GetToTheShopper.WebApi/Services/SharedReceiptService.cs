using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using GetToTheShopper.WebApi.Exceptions;
using GetToTheShopper.WebApi.DTO;

namespace GetToTheShopper.WebApi.Services
{
    public class SharedReceiptService
    {
        private GetToTheShopperContext context;

        public SharedReceiptService(GetToTheShopperContext context)
        {
            this.context = context;
        }

        public SharedReceipt GetSharedReceiptById(int id)
        {

            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.SharedReceipts.GetAll().Where(sr => sr.Id == id).SingleOrDefault();
            }
        }
        public SharedReceipt GetSharedReceiptBySecondaryKey(int receiptId, string userId)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.SharedReceipts.GetAll().Where(sr => sr.ReceiptId == receiptId 
                && sr.UserId == userId).SingleOrDefault();
            }
        }
        public bool ReceiptSharedToUser(int receiptId, string userId)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.SharedReceipts.Any(
                        sr => sr.ReceiptId == receiptId && sr.UserId == userId)
                    || unitOfWork.Receipts.Any(
                        r => r.Id == receiptId && r.AuthorId == userId);
            }
        }
        public List<SharedReceipt> GetSharedReceiptList()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.SharedReceipts.GetAll().ToList();
            }
        }
        public List<SharedReceipt> GetSharedReceiptListByUserId(string userId)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return GetSharedReceiptList().Where(sr => sr.UserId == userId).ToList();
            }
        }

        public List<Receipt> GetReceiptListByUserId(string userId)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                var shared = unitOfWork.SharedReceipts.GetAll().Where(sr => sr.UserId == userId);
                return unitOfWork.Receipts.Where(r => shared.Any(sr => sr.ReceiptId == r.Id)).ToList();
            }
        }

        public List<SharedReceipt> GetSharedReceiptListByAuthorId(string userId, int receiptId)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.SharedReceipts.Where(sr => sr.Receipt.AuthorId == userId && sr.ReceiptId == receiptId).ToList();
            }
        }

        public void AddSharedReceipt(SharedReceipt SharedReceipt)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                if (unitOfWork.SharedReceipts.FirstOrDefault(p => p.ReceiptId == SharedReceipt.ReceiptId && p.UserId == SharedReceipt.UserId) != null)
                    throw new AttributeAlreadyExistsException("SharedReceipt", "name");
                unitOfWork.SharedReceipts.Add(SharedReceipt);
                unitOfWork.Save();
            }
        }

        public void DeleteSharedReceipt(SharedReceipt SharedReceipt)
        {
            if (SharedReceipt != null)
            {
                using (var unitOfWork = new UnitOfWork(context))
                {
                    unitOfWork.SharedReceipts.Remove(SharedReceipt);
                    unitOfWork.Save();
                }
            }
        }


    }
}
