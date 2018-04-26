using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using GetToTheShopper.WebApi.Exceptions;

namespace GetToTheShopper.WebApi.Services
{
    public class ProductService
    {
        private GetToTheShopperContext context;

        public ProductService(GetToTheShopperContext context)
        {
            this.context = context;
        }

        internal Product GetProductById(int productId)
        {
            if (productId < 0) //default
            {
                return GetDefaultProduct();
            }
            using (var unitOfWork = new UnitOfWork(context))
            {
                var product = unitOfWork.Products.Find(productId);
                if (product == null)
                    throw new NonExistingRecordException("Product", "id");
                return product;
            }
        }
        public Product GetDefaultProduct()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                int index = 1;
                while (unitOfWork.Products.Any(p => p.Name == "New Product " + index.ToString()))
                    index++;
                return new Product() { Name = "New Product " + index.ToString() };
            }
        }
        public List<Product> GetProductList()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.Products.GetAll().ToList();
            }
        }

        public void AddProduct(Product product)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                if (unitOfWork.Products.FirstOrDefault(p => p.Name == product.Name) != null)
                    throw new AttributeAlreadyExistsException("Product", "name");
                unitOfWork.Products.Add(product);
                unitOfWork.Save();
            }
        }

        public void EditProduct(Product product)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.Products.UpdateByObject(product);
                unitOfWork.Save();
            }
        }

        public void DeleteProduct(Product product)
        {
            if (product != null)
            {
                using (var unitOfWork = new UnitOfWork(context))
                {
                    unitOfWork.Products.Remove(product);
                    unitOfWork.Save();
                }
            }
        }
    }
}
