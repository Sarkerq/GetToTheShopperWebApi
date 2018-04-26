using ShoppingList.Core.Model;
using ShoppingList.Core.ModelHelper;
using ShoppingList.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ShoppingList.Core.Repositories.Implementations
{
    class ReceiptRepository : Repository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(DbContext context) : base(context)
        {
        }

        public GetToTheShopperContext SContext { get => Context as GetToTheShopperContext; }

        public IEnumerable<Product> GetFilteredProductsNotOnList(Receipt receipts, string filterString)
        {
            var query = SContext.Products.AsQueryable();

            if(filterString != "" && filterString != null)
            {
                query = query.Where(p => p.Name.ToLower().Contains(filterString.ToLower()));
            }

            query = query.Where(p => !(
                SContext.ReceiptProducts
                .Where(slp => slp.ProductId == p.Id && slp.ReceiptId == receipts.Id)
                .Any())
                );
            
            return query.OrderBy(p => p.Name).ToList();
        }

        public IEnumerable<ProductFromListInShop> GetProductsFromListInShop(Receipt receipts)
        {
            return (from slp in SContext.ReceiptProducts
                    join sl in SContext.Receipts on slp.ReceiptId equals sl.Id
                    join sp in SContext.ShopProducts on slp.ProductId equals sp.ProductId into joined
                    from j in joined.DefaultIfEmpty()
                    where sl.Id == receipts.Id
                    select new ProductFromListInShop
                    {
                        ReceiptProduct = slp,
                        ShopProduct = j,
                        Product = slp.Product,
                        WantedQuantity = slp.Quantity,
                        EnoughUnits = (j == null ? (bool?)null : j.AvailableUnits >= slp.Quantity)
                    }).ToList();
        }
    }
}
