using ShoppingList.Core.Model;
using ShoppingList.Core.ModelHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Repositories.Interfaces
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        IEnumerable<ProductFromListInShop> GetProductsFromListInShop(Receipt receipts);
        IEnumerable<Product> GetFilteredProductsNotOnList(Receipt receipts, string filterString);
    }
}
