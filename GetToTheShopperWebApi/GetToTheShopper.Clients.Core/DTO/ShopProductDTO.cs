using System;
using System.Collections.Generic;
using System.Text;

namespace GetToTheShopper.Clients.Core.DTO
{
    public class ShopProductDTO
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public ProductDTO Product { get; set; }
        public double AvailableUnits { get; set; }
        public string Aisle { get; set; }

        public ShopProductDTO()
        { }
        public ShopProductDTO(ShopProductDTO pattern)
        {
            Id = pattern.Id;
            ShopId = pattern.ShopId;
            AvailableUnits = pattern.AvailableUnits;
            Aisle = pattern.Aisle;
            Product = new ProductDTO(pattern.Product);
        }
    }
}
