using GetToTheShopper.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO.Assemblers
{
    public class ShopAssembler
    {
        public ShopDTO GetDTO(Shop shop)
        {
            return new ShopDTO()
            {
                Id = shop.Id,
                Name = shop.Name,
                Address = shop.Address,
                Latitude = shop.Latitude,
                Longitude = shop.Longitude
            };
        }

        public Shop GetModel(ShopDTO shopDTO)
        {
            return new Shop()
            {
                Id = shopDTO.Id,
                Name = shopDTO.Name,
                Address = shopDTO.Address,
                Latitude = shopDTO.Latitude,
                Longitude = shopDTO.Longitude
            };
        }
    }
}
