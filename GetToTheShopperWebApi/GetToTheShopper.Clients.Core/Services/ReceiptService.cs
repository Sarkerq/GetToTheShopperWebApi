using GetToTheShopper.Clients.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.Services
{
    public class ReceiptService
    {
        public bool AddReceipt(ReceiptDTO receipt)
        {

            return WebClient.PostAsync<ReceiptDTO>("Receipt", receipt).Result;
        }

        public bool UpdateReceipt(ReceiptDTO receipt)
        {
            return WebClient.PutAsync<ReceiptDTO>("Receipt/" + receipt.Id, receipt).Result;
        }


        public bool DeleteReceipt(ReceiptDTO receipt)
        {
            return WebClient.DeleteAsync<ShopProductDTO>("Receipt/" + receipt.Id).Result;
        }
        public ReceiptDTO GetReceipt(int id)
        {
            return WebClient.ReadResponse<ReceiptDTO>("Receipt/" + id).Result;
        }
        public ReceiptDTO GetDefaultReceipt()
        {
            return WebClient.ReadResponseSync<ReceiptDTO>("Receipt/-1");
        }
        public IEnumerable<ReceiptDTO> GetAllReceipts()
        {
            return WebClient.ReadResponse<IEnumerable<ReceiptDTO>>("Receipt").Result;
        }

        public bool ReceiptNameAlreadyExists(string value)
        {
            return GetAllReceipts().Any(s => s.Name == value);
        }

        public IEnumerable<ProductFromListInShopDTO> GetProductsFromListInShop(ReceiptDTO receipt, ShopDTO shop)
        {
            return WebClient.ReadResponse<IEnumerable<ProductFromListInShopDTO>>("Receipt/ProductsFromListInShop/" + receipt.Id + "/" + shop.Id).Result;
        }

        public IEnumerable<ReceiptInShopDTO> GetReceiptInShops(ReceiptDTO receipt)
        {
            return WebClient.ReadResponse<IEnumerable<ReceiptInShopDTO>>("Receipt/ReceiptInShops/" + receipt.Id).Result;
        }

        public IEnumerable<ProductDTO> GetFilteredProductsNotOnList(ReceiptDTO receipt, string filterString)
        {
            var productService = new ProductService();
            var receiptProductService = new ReceiptProductService();
            var products = productService.GetProductList();
            var receiptProducts = receiptProductService.GetReceiptProductsFromReceipt(receipt.Id).AsQueryable();
            var query = products.AsQueryable();

            if (filterString != "" && filterString != null)
            {
                query = query.Where(p => p.Name.ToLower().Contains(filterString.ToLower()));
            }

            query = query.Where(p => !(
                receiptProducts
                .Where(slp => slp.ProductId == p.Id && slp.ReceiptId == receipt.Id)
                .Any())
                );

            return query.OrderBy(p => p.Name).ToList();
        }

        public bool ChangeDoneState(ReceiptDTO receipt)
        {
            receipt.Done = !receipt.Done;
            return UpdateReceipt(receipt);
        }
    }
}
