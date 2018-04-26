using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.ModelsHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Repositories.Interfaces
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        IEnumerable<ProductFromListInShop> GetProductsFromListInShop(Receipt receipts, int shopId);
        IEnumerable<Product> GetFilteredProductsNotOnList(Receipt receipts, string filterString);
        IEnumerable<ReceiptInShopDTO> GetReceiptInShops(Receipt receipt);
    }
}
