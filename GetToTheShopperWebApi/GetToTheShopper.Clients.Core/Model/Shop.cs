using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Model
{
    public class Shop : ModelBase
    {
        public int Id { get; set; }

        public virtual ICollection<ShopProduct> ShopProducts { get; set; }
    }
}
