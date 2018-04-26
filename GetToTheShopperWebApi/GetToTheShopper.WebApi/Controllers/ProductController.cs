using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.DTO.Assemblers;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Services;
using GetToTheShopper.WebApi.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebApplication1.Models.AccountViewModels;
using System.Security.Principal;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GetToTheShopper.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
#if TEST
#else
    [Authorize]
#endif
    public class ProductController : Controller
    {
        private ProductService service;
        private ProductAssembler assembler;
        public ProductController(GetToTheShopperContext context)
        {
            service = new ProductService(context);
            assembler = new ProductAssembler();
        }

        // GET: api/Product
        [HttpGet(Name = "GetProducts")]
        public IEnumerable<ProductDTO> Get()
        {
            var products = service.GetProductList();
            return from p in products
                   select assembler.GetDTO(p);
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "GetProduct")]
        public ProductDTO Get(int id)
        {
            return assembler.GetDTO(service.GetProductById(id));
        }

        // POST: api/Product
        [HttpPost]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Post([FromBody]ProductDTO value)
        {
            service.AddProduct(assembler.GetModel(value));
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Put(int id, [FromBody]ProductDTO value)
        {
            service.EditProduct(assembler.GetModel(value));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Delete(int id)
        {
            service.DeleteProduct(service.GetProductById(id));
        }
    }

}
