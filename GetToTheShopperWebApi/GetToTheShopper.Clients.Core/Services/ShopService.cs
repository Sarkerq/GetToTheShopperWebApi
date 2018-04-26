using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetToTheShopper.Clients.Core.DTO;

namespace GetToTheShopper.Clients.Core.Services
{
    public class ShopService
    {
        public IEnumerable<ShopDTO> GetShopsList()
        {
            return WebClient.ReadResponse<List<ShopDTO>>("Shop").Result;
        }
        public bool ShopNameAlreadyExists(string value)
        {
            return GetShopsList().Any(s => s.Name == value);
        }
        public ShopDTO Get(int id)
        {
            return WebClient.ReadResponse<ShopDTO>("Shop/" + id).Result;
        }
        public ShopDTO GetDefaultShop()
        {
            return WebClient.ReadResponse<ShopDTO>("Shop/-1").Result;
        }

        public bool AddShop(ShopDTO shop)
        {
            return WebClient.PostAsync<ShopDTO>("Shop", shop).Result;
        }

        public bool EditShop(ShopDTO shop)
        {
            return WebClient.PutAsync<ShopDTO>("Shop/" + shop.Id, shop).Result;
        }

        public bool DeleteShop(ShopDTO shop)
        {
            return WebClient.DeleteAsync<ShopDTO>("Shop/" + shop.Id).Result;
        }

        public IEnumerable<ProductDTO> GetFilteredProductsNotInShop(ShopDTO shop, string filterString)
        {
            var productService = new ProductService();
            var shopProductService = new ShopProductService();
            var receiptProductService = new ReceiptProductService();
            var products = productService.GetProductList();
            var shopProducts = shopProductService.GetShopProductListByShopId(shop.Id).AsQueryable();
            var query = products.AsQueryable();

            if (filterString != "" && filterString != null)
            {
                query = query.Where(p => p.Name.ToLower().Contains(filterString.ToLower()));
            }

            query = query.Where(p => !(
                shopProducts
                .Where(sp => sp.Product.Id == p.Id && sp.ShopId == shop.Id)
                .Any())
                );

            return query.OrderBy(p => p.Name).ToList();
        }
    }
}
