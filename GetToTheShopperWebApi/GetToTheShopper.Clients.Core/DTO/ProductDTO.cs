using System;
using System.Collections.Generic;
using System.Text;

namespace GetToTheShopper.Clients.Core.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Unit Unit { get; set; }
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
