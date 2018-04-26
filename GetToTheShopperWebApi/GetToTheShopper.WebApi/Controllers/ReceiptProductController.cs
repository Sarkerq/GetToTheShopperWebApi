using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.Services;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.DTO.Assemblers;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;

namespace GetToTheShopper.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ReceiptProduct")]
#if TEST
#else
    [Authorize]
#endif
    public class ReceiptProductController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private ProductService productService;
        private ReceiptProductService service;
        private ReceiptService receiptService;
        private SharedReceiptService sharedReceiptService;

        private ReceiptProductAssembler assembler;

        public ReceiptProductController(GetToTheShopperContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            service = new ReceiptProductService(context);
            receiptService = new ReceiptService(context);
            productService = new ProductService(context);
            sharedReceiptService = new SharedReceiptService(context);
            assembler = new ReceiptProductAssembler();
        }

        // GET: api/ReceiptProduct/5
        [HttpGet("{receiptId}", Name = "GetReceiptProductsFromReceipt")]
        public IEnumerable<ReceiptProductDTO> Get(int receiptId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            if (sharedReceiptService.ReceiptSharedToUser(receiptId,userId))
            {
                var receiptProducts = service.GetReceiptProductList(receiptService.GetReceiptById(receiptId));
                return from rp in receiptProducts
                       select assembler.GetDTO(rp);
            }
            else
                return null;
        }

        // POST: api/ReceiptProduct
        [HttpPost]
        public bool Post([FromBody]ReceiptProductDTO value)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            if (receiptService.GetReceiptById(value.ReceiptId).AuthorId == userId)
            {
                service.AddProductToList(assembler.GetModel(value));
                return true;
            }
            else
                return false;
        }
        
        // PUT: api/ReceiptProduct/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]ReceiptProductDTO value)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            if (receiptService.GetReceiptById(value.ReceiptId).AuthorId == userId)
            {
                service.UpdateProductOnList(assembler.GetModel(value));
                return true;
            }
            else
                return false;
        }

        // DELETE: api/ReceiptProduct/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            if (receiptService.GetReceiptById(service.GetReceiptProductById(id).ReceiptId).AuthorId == userId)
            {
                service.DeleteProductFromList(service.GetReceiptProductById(id));
                return true;
            }
            else
                return false;
        }
    }
}
