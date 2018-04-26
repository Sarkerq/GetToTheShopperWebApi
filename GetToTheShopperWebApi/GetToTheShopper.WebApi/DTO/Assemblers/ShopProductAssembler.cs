using GetToTheShopper.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO.Assemblers
{
    public class ShopProductAssembler
    {
        public ShopProductDTO GetDTO(ShopProduct shopProduct)
        {
            if (shopProduct == null) return null;
            return new ShopProductDTO()
            {
                Id = shopProduct.Id,
                ShopId = shopProduct.ShopId,
                Product = new ProductDTO()
                {
                    Id = shopProduct.Product.Id,
                    Name = shopProduct.Product.Name,
                    Price = shopProduct.Product.Price,
                    Unit = new Unit(shopProduct.Product.Unit)
                },
                Aisle = shopProduct.Aisle,
                AvailableUnits = shopProduct.AvailableUnits,

            };
        }

        public ShopProduct GetModel(ShopProductDTO shopProductDTO)
        {
            var product = new Product()
            {
                Id = shopProductDTO.Product.Id,
                Name = shopProductDTO.Product.Name,
                Price = shopProductDTO.Product.Price,
                Unit = shopProductDTO.Product.Unit.UnitType
            };
            return new ShopProduct()
            {
                Id = shopProductDTO.Id,
                ShopId = shopProductDTO.ShopId,
                ProductId = product.Id,
                Aisle = shopProductDTO.Aisle,
                AvailableUnits = shopProductDTO.AvailableUnits
            };
        }
    }
}
