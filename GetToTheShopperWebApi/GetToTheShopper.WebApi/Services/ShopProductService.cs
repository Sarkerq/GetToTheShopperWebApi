using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Services
{
    public class ShopProductService
    {
        GetToTheShopperContext context;
        public ShopProductService(GetToTheShopperContext context)
        {
            this.context = context;
        }

        public ShopProduct GetShopProductById(int id)
        {
            if(id < 0) //default
            {
                return GetDefaultShopProduct();
            }
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.ShopProducts.Include(ShopProduct => ShopProduct.Product).SingleOrDefault(sp => sp.Id == id);
            }
        }
        public List<ShopProduct> GetShopProductList()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.ShopProducts.Include(ShopProduct => ShopProduct.Product).ToList();
            }
        }
        public List<ShopProduct> GetShopProductListByShopId(int shopId)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.ShopProducts.Include(ShopProduct => ShopProduct.Product)
                    .Where(ShopProduct => ShopProduct.ShopId == shopId).ToList();
            }
        }
        public ShopProduct GetDefaultShopProduct()
        {
            var shopProduct = new ShopProduct() { Aisle = "1A" };
            using (var unitOfWork = new UnitOfWork(context))
            {
                int index = 0;
                while (unitOfWork.Products.Any(p => p.Name == "New Product " + index.ToString()))
                    index++;
                shopProduct.Product = new Product() { Name = "New Product " + index.ToString() };
            }
            return shopProduct;
        }

        public void AddShopProduct(ShopProduct shopProduct)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.ShopProducts.Add(shopProduct);
                unitOfWork.Save();
            }
        }

        public void EditShopProduct(ShopProduct shopProduct)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.ShopProducts.UpdateByObject(shopProduct);
                unitOfWork.Save();
            }
        }

        public void DeleteShopProduct(ShopProduct shopProduct)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.ShopProducts.Remove(shopProduct);
                unitOfWork.Save();
            }

        }
    }
}
