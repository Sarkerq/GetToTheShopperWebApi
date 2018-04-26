using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GetToTheShopper.WebApi.Services;
using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.DTO.Assemblers;
using Microsoft.AspNetCore.Authorization;

namespace GetToTheShopper.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ShopProduct")]
#if TEST
#else
    [Authorize]
#endif
    public class ShopProductController : Controller
    {
        private ShopProductService service;
        private ShopProductAssembler assembler;

        public ShopProductController(GetToTheShopperContext context)
        {
            service = new ShopProductService(context);
            assembler = new ShopProductAssembler();
        }

        // GET: api/ShopProduct
        [HttpGet(Name = "GetShopProducts")]
        public IEnumerable<ShopProductDTO> Get()
        {
            var shopProducts = service.GetShopProductList();
            return from sp in shopProducts
                   select assembler.GetDTO(sp);
        }
        // GET: api/ShopProduct/Shop/5
        [HttpGet("Shop/{id}", Name = "GetShopProductsByShopId")]
        public IEnumerable<ShopProductDTO> GetByShopId(int id)
        {
            var shopProducts = service.GetShopProductListByShopId(id);
            return from sp in shopProducts
                   select assembler.GetDTO(sp);
        }
        // GET: api/ShopProduct/5
        [HttpGet("{id}", Name = "GetShopProduct")]
        public ShopProductDTO Get(int id)
        {
            return assembler.GetDTO(service.GetShopProductById(id));
        }

        // POST: api/ShopProduct
        [HttpPost]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Post([FromBody]ShopProductDTO value)
        {
            service.AddShopProduct(assembler.GetModel(value));
        }

        // PUT: api/ShopProduct/5
        [HttpPut("{id}")]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Put(int id, [FromBody]ShopProductDTO value)
        {
            service.EditShopProduct(assembler.GetModel(value));
        }

        // DELETE: api/ShopProduct/5
        [HttpDelete("{id}")]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Delete(int id)
        {
            service.DeleteShopProduct(service.GetShopProductById(id));
        }
    }
}
