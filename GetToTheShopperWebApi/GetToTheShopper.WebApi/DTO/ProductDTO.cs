using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetToTheShopper.WebApi.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Unit Unit { get; set; }
        [Required]
        [Range(0, Double.MaxValue)]
        public double Price { get; set; }

        public ProductDTO() { }

        public ProductDTO(ProductDTO pattern)
        {
            Id = pattern.Id;
            Name = pattern.Name;
            Unit = pattern.Unit;
            Price = pattern.Price;
        }
    }
}
