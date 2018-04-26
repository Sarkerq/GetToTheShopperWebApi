using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.ModelsHelpers;
using GetToTheShopper.WebApi.Repositories.Interfaces;
using GetToTheShopper.WebApi.DTO;

namespace GetToTheShopper.WebApi.Repositories.Implementations
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

        public IEnumerable<ProductFromListInShop> GetProductsFromListInShop(Receipt receipt, int shopId)
        {
            var tmp1 = (from slp in SContext.ReceiptProducts
                    join sl in SContext.Receipts on slp.ReceiptId equals sl.Id
                    join sp in (from spp in SContext.ShopProducts where spp.ShopId == shopId select spp) on slp.ProductId equals sp.ProductId into joined
                    from j in joined.DefaultIfEmpty()
                    where sl.Id == receipt.Id
                    select new ProductFromListInShop
                    {
                        ReceiptProduct = slp,
                        ShopProduct = j,
                        Product = slp.Product,
                        WantedQuantity = slp.Quantity,
                        EnoughUnits = (j == null ? (bool?)null : j.AvailableUnits >= slp.Quantity)
                    });
            return tmp1.ToList();
        }

        public IEnumerable<ReceiptInShopDTO> GetReceiptInShops(Receipt receipt)
        {
            var countReceiptProducts = (from sp in SContext.ReceiptProducts
                                   where sp.ReceiptId == receipt.Id
                                   select new { id = sp.ProductId }).Count();
            if (countReceiptProducts == 0)
                return null;
            var tmp = (from s in SContext.Shops
                        select new ReceiptInShopDTO
                        {
                            Shop = new ShopDTO
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Address = s.Address,
                                Latitude = s.Latitude,
                                Longitude = s.Longitude
                            },
                            Availability = Double.Parse((from slp in SContext.ReceiptProducts
                                                 join sl in SContext.Receipts on slp.ReceiptId equals sl.Id
                                                 join sp in SContext.ShopProducts on slp.ProductId equals sp.ProductId into joined
                                                 from j in joined.DefaultIfEmpty()
                                                 join ss in SContext.Shops on j.ShopId equals ss.Id
                                                 where sl.Id == receipt.Id && ss.Id == s.Id
                                                 select new
                                                 {
                                                     EnoughUnits = (j == null ? (bool?)null : j.AvailableUnits >= slp.Quantity)
                                                 }).Count(x => x.EnoughUnits == true).ToString()) / countReceiptProducts
                        }
                    );
            var tmp2 =  tmp.ToList();
            return tmp2;
        }
    }
}
