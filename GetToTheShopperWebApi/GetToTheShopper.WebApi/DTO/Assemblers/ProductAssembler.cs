using GetToTheShopper.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO.Assemblers
{
    public class ProductAssembler
    {
        public ProductDTO GetDTO(Product product)
        {
            return new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Unit = new Unit(product.Unit)
            };
        }

        public Product GetModel(ProductDTO productDTO)
        {
            return new Product()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Unit = productDTO.Unit.UnitType
            };
        }
    }
}
