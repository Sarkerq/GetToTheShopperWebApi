using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.Services;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.ModelsHelpers;
using GetToTheShopper.WebApi.DTO.Assemblers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace GetToTheShopper.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Receipt")]
#if TEST
#else
    [Authorize]
#endif
    public class ReceiptController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private ReceiptService service;
        private SharedReceiptService sharedReceiptService;

        private ShopService shopService;
        private ReceiptAssembler assembler;
        private ProductAssembler productAssembler;
        private ProductFromListInShopAssembler productFromListInShopAssembler;

        public ReceiptController(GetToTheShopperContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            service = new ReceiptService(context);
            shopService = new ShopService(context);
            sharedReceiptService = new SharedReceiptService(context);
            assembler = new ReceiptAssembler();
            productAssembler = new ProductAssembler();
            productFromListInShopAssembler = new ProductFromListInShopAssembler();
        }

        // GET: api/Receipt/
        [HttpGet(Name = "GetReceipts")]
        public IEnumerable<ReceiptDTO> Get()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            var receipts = service.GetUsersReceipts(userId);
            return from r in receipts
                   select assembler.GetDTO(r);
        }
        // GET: api/Receipt/5
        [HttpGet("{receiptId:int}", Name = "GetReceipt")]
        public ReceiptDTO Get(int receiptId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (sharedReceiptService.ReceiptSharedToUser(receiptId, userId))
                return assembler.GetDTO(service.GetReceiptById(receiptId));
            else
                return null;
        }

        // GET: api/Receipt/ProductsFromListInShop/5/5
        [HttpGet("ProductsFromListInShop/{receiptId}/{shopId}", Name = "ProductsFromListInShop")]
        public IEnumerable<ProductFromListInShopDTO> GetProductsFromListInShop(int receiptId, int shopId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (sharedReceiptService.ReceiptSharedToUser(receiptId, userId))
            {
                var products = service.GetProductsFromListInShop(service.GetReceiptById(receiptId), shopId);
                return from pflis in products
                       select productFromListInShopAssembler.GetDTO(pflis);
            }
            else
                return null;
        }

        // GET: api/Receipt/ReceiptInShops/5
        [HttpGet("ReceiptInShops/{receiptId}", Name = "ReceiptInShops")]
        public IEnumerable<ReceiptInShopDTO> GetReceiptInShops(int receiptId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (sharedReceiptService.ReceiptSharedToUser(receiptId, userId))
                return service.GetReceiptInShops(service.GetReceiptById(receiptId));
            else
                return null;
        }

        // POST: api/Receipt
        [HttpPost]
        public void Post([FromBody]ReceiptDTO value)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            value.AuthorId = userId;
            service.AddReceipt(assembler.GetModel(value));
        }

        // PUT: api/Receipt/5
        [HttpPut("{receiptId}")]
        public bool Put(int receiptId, [FromBody]ReceiptDTO value)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            if (value.AuthorId == userId)
            {
                service.UpdateReceipt(assembler.GetModel(value));
                return true;
            }
            else
                return false;
        }
        // PUT: api/Receipt/ChangeDone/5
        [HttpPut("ChangeDone/{receiptId}")]
        public bool ChangeDone(int receiptId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (sharedReceiptService.ReceiptSharedToUser(receiptId, userId))
            {
                Receipt toChange = service.GetReceiptById(receiptId);
                service.ChangeDoneState(toChange);
                return true;
            }
            else
                return false;
        }
        // DELETE: api/Receipt/5
        [HttpDelete("{receiptId}")]
        public bool Delete(int receiptId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            Receipt toDelete = service.GetReceiptById(receiptId);
            if (toDelete.AuthorId == userId)
            {
                service.DeleteReceipt(toDelete);
                return true;
            }
            else
                return false;
        }
    }
}
