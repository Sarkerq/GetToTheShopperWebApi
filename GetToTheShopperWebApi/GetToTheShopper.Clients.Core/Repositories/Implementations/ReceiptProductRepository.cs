using ShoppingList.Core.Model;
using ShoppingList.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ShoppingList.Core.Repositories.Implementations
{
    class ReceiptProductRepository : Repository<ReceiptProduct>, IReceiptProductRepository
    {
        public ReceiptProductRepository(DbContext context) : base(context)
        {
        }

        public GetToTheShopperContext SContext { get => Context as GetToTheShopperContext; }

        public new void Add(ReceiptProduct receiptsProduct)
        {
            receiptsProduct.Receipt = SContext.Receipts.Find(receiptsProduct.ReceiptId);
            receiptsProduct.Product = SContext.Products.Find(receiptsProduct.ProductId);
            SContext.ReceiptProducts.Add(receiptsProduct);
        }
    }
}
