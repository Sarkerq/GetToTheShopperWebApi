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
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace GetToTheShopper.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/SharedReceipt")]
#if TEST
#else
    [Authorize]
#endif
    public class SharedReceiptController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private SharedReceiptService service;
        private ReceiptService receiptService;
        private UserService userService;
        private SharedReceiptAssembler assembler;
        private ReceiptAssembler receiptAssembler;
        private UserAssembler userAssembler;

        public SharedReceiptController(GetToTheShopperContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            service = new SharedReceiptService(context);
            receiptService = new ReceiptService(context);
            userService = new UserService(context);
            assembler = new SharedReceiptAssembler();
            receiptAssembler = new ReceiptAssembler();
            userAssembler = new UserAssembler();
        }

        // GET: api/SharedReceipt/GetSharedForCurrentUser
        [HttpGet("GetSharedForCurrentUser", Name = "GetSharedForCurrentUser")]
        public IEnumerable<ReceiptDTO> GetSharedForCurrentUser()
        {
            var receipts = service.GetReceiptListByUserId(_userManager.GetUserId(HttpContext.User));
            return from rp in receipts
                   select receiptAssembler.GetDTO(rp);
        }

        // GET: api/SharedReceipt/GetUsersWithAccessToReceipt/5
        [HttpGet("GetUsersWithAccessToReceipt/{receiptId}", Name = "GetUsersWithAccessToReceipt")]
        public IEnumerable<UserDTO> GetUsersWithAccessToReceipt(int receiptId)
        {
            var SharedReceipts = service.GetSharedReceiptList().Where(sr => sr.ReceiptId == receiptId);
            return from sr in SharedReceipts
                   select userAssembler.GetDTO(userService.GetUserById(sr.UserId));
        }


        // POST: api/SharedReceipt
        [HttpPost]
        public bool Post([FromBody]SharedReceiptDTO value)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (receiptService.GetReceiptById(value.ReceiptId).AuthorId == userId)
            {
                service.AddSharedReceipt(assembler.GetModel(value));
                return true;
            }
            else
                return false;
        }
        // DELETE: api/SharedReceipt
        [HttpDelete("{receiptId}/{userToShareTo}")]
        public bool Delete(int receiptId, string userToShareTo)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (receiptService.GetReceiptById(receiptId).AuthorId == userId)
            {
                service.DeleteSharedReceipt(service.GetSharedReceiptBySecondaryKey(receiptId, userToShareTo));
                return true;
            }
            else
                return false;
        }
    }
}
