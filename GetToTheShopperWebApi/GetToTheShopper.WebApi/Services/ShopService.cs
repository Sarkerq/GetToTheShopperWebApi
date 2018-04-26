using GetToTheShopper.WebApi.Exceptions;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Services
{
    public class ShopService
    {
        GetToTheShopperContext context;
        public ShopService(GetToTheShopperContext context)
        {
            this.context = context;
        }

        public Shop GetShopById(int id)
        {
            if (id < 0) //default
            {
                return GetDefaultShop();
            }
            using (var unitOfWork = new UnitOfWork(context))
            {
                var shop = unitOfWork.Shops.Find(id);
                if (shop == null)
                    throw new NonExistingRecordException("Shop", "id");
                return shop;
            }
        }
        public Shop GetDefaultShop()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                int index = 1;
                while (unitOfWork.Shops.Any(r => r.Name == "New Shop " + index.ToString()))
                    index++;
                return new Shop() { Name = "New Shop " + index.ToString() };
            }
        }

        public List<Shop> GetShopsList()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.Shops.GetAll().ToList();
            }
        }

        public void AddShop(Shop shop)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                if (unitOfWork.Shops.FirstOrDefault(p => p.Name == shop.Name) != null)
                    throw new AttributeAlreadyExistsException("Shop", "name");

                unitOfWork.Shops.Add(shop);
                unitOfWork.Save();
            }
        }

        public void EditShop(Shop shop)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.Shops.UpdateByObject(shop);
                unitOfWork.Save();
            }
        }

        public void DeleteShop(Shop shop)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.Shops.Remove(shop);
                unitOfWork.Save();
            }
        }
    }
}
