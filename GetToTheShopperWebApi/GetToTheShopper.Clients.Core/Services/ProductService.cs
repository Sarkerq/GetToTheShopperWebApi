using GetToTheShopper.Clients.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.Services
{
    public class ProductService
    {
        public IEnumerable<ProductDTO> GetProductList()
        {
            return WebClient.ReadResponse<List<ProductDTO>>("Product").Result;
        }

        public ProductDTO GetProduct(int id)
        {
            return WebClient.ReadResponse<ProductDTO>("Product/" + id).Result;
        }
        public bool ProductNameAlreadyExists(string value)
        {
            return GetProductList().Any(s => s.Name == value);
        }
        public ProductDTO GetDefaultProduct()
        {
            return WebClient.ReadResponseSync<ProductDTO>("Product/-1");
        }
        public bool AddProduct(ProductDTO Product)
        {
            return WebClient.PostAsync<ProductDTO>("Product", Product).Result;
        }
        public bool EditProduct(ProductDTO Product)
        {
            return WebClient.PutAsync<ProductDTO>("Product/" + Product.Id, Product).Result;
        }

        public bool DeleteProduct(ProductDTO Product)
        {
            return WebClient.DeleteAsync<ProductDTO>("Product/" + Product.Id).Result;
        }
    }
}
