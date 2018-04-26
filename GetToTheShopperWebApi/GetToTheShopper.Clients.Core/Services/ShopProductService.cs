using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GetToTheShopper.Clients.Core.DTO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GetToTheShopper.Clients.Core.Services
{
    public class ShopProductService
    {
        public List<ShopProductDTO> GetShopProductList()
        {
            return WebClient.ReadResponse<List<ShopProductDTO>>("ShopProduct").Result;
        }
        public List<ShopProductDTO> GetShopProductListByShopId(int shopId)
        {
            return WebClient.ReadResponse<List<ShopProductDTO>>("ShopProduct/Shop/" + shopId).Result;
        }
        public ShopProductDTO GetShopProduct(int id)
        {
            return WebClient.ReadResponse<ShopProductDTO>("ShopProduct/" + id).Result;
        }
        public ShopProductDTO GetDefaultShopProduct()
        {
            return WebClient.ReadResponseSync<ShopProductDTO>("ShopProduct/-1");

        }
        public bool AddShopProduct(ShopProductDTO shopProduct)
        {
            return WebClient.PostAsync<ShopProductDTO>("ShopProduct", shopProduct).Result;
        }

        public bool EditShopProduct(ShopProductDTO shopProduct)
        {
            return WebClient.PutAsync<ShopProductDTO>("ShopProduct/" + shopProduct.Id, shopProduct).Result;
        }

        public bool DeleteShopProduct(ShopProductDTO shopProduct)
        {
            return WebClient.DeleteAsync<ShopProductDTO>("ShopProduct/" + shopProduct.Id).Result;
        }
    }
}
