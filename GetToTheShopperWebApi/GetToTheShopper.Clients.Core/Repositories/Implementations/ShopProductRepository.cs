using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Core.Model;
using ShoppingList.Core.Repositories.Interfaces;
using System.Data.Entity;

namespace ShoppingList.Core.Repositories.Implementations
{
    public class ShopProductRepository : Repository<ShopProduct>, IShopProductRepository
    {

        public ShopProductRepository(DbContext context) : base(context)
        {
        }
        public GetToTheShopperContext SContext { get => Context as GetToTheShopperContext; }

    }
}
