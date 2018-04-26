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
    public class ShopRepository : Repository<Shop>, IShopRepository
    {
        public ShopRepository(DbContext context) : base(context)
        {
        }
    }
}
