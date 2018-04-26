using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Services;
using GetToTheShopper.WebApi.DTO.Assemblers;
using Microsoft.AspNetCore.Authorization;

namespace GetToTheShopper.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Shop")]
#if TEST
#else
    [Authorize]
#endif
    public class ShopController : Controller
    {
        private ShopService service;
        private ShopAssembler assembler;

        public ShopController(GetToTheShopperContext context)
        {
            service = new ShopService(context);
            assembler = new ShopAssembler();
        }

        // GET: api/Shop
        [HttpGet(Name = "GetShops")]
        public IEnumerable<ShopDTO> Get()
        {
            var shops = service.GetShopsList();
            return from s in shops
                   select assembler.GetDTO(s);
        }

        // GET: api/Shop/5
        [HttpGet("{id}", Name = "Get")]
        public ShopDTO Get(int id)
        {
            return assembler.GetDTO(service.GetShopById(id));
        }

        // POST: api/Shop
        [HttpPost]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Post([FromBody]ShopDTO value)
        {
            service.AddShop(assembler.GetModel(value));
        }

        // PUT: api/Shop/5
        [HttpPut("{id}")]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Put(int id, [FromBody]ShopDTO value)
        {
            service.EditShop(assembler.GetModel(value));
        }

        // DELETE: api/Shop/5
        [HttpDelete("{id}")]
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        public void Delete(int id)
        {
            service.DeleteShop(service.GetShopById(id));
        }
    }
}
