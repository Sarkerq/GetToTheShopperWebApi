using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetToTheShopper.WebApi.DTO
{
    public class ShopProductDTO
    {
        public int Id { get; set; }
        [Required]
        public int ShopId { get; set; }
        [Required]
        public ProductDTO Product { get; set; }
        [Required]
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
