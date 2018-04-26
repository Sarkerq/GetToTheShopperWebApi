using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.DTO.Assemblers;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication1.Models;
using WebApplication1.Models.AccountViewModels;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
#if TEST
#else
    [Authorize]
#endif
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private UserService service;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            GetToTheShopperContext context )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            service = new UserService(context);
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Login([FromBody]LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, lockoutOnFailure: false);
                return result.Succeeded;
            }
            return false;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Register([FromBody]RegisterDTO model)
        {

            if (ModelState.IsValid && model.UserRoles == "Client")
            {
                var user = new ApplicationUser { UserName = model.Login };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, model.UserRoles);
                }
                AddErrors(result);
                return result.Succeeded;
            }
            return false;
        }

        [HttpPost]
        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }
#if TEST
#else
        [Authorize(Roles = "Seller")]
#endif
        [HttpPost]
        public async Task<bool> RegisterSeller([FromBody]RegisterDTO model)
        {

            if (ModelState.IsValid && model.UserRoles == "Seller")
            {
                var user = new ApplicationUser { UserName = model.Login };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, model.UserRoles);
                }
                AddErrors(result);
                return result.Succeeded;
            }
            return false;
        }

#if TEST
#else
        [Authorize]
#endif
        [HttpGet]
        public IEnumerable<UserDTO> GetLogins()
        {
            string userName =_userManager.GetUserName(HttpContext.User);
            var assembler = new UserAssembler();
            var users = service.GetClientsListExcept(userName);
            return from user in users
                   select assembler.GetDTO(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<UserDTO> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return new UserDTO { Username = _userManager.GetUserName(HttpContext.User), Roles = roles.ToArray() };
            }
            else
                return new UserDTO();
        }

        [HttpPost]
        [AllowAnonymous]
        public bool CheckIfUserNameAvailable([FromBody]StringDTO login)
        {
            return service.CheckIfUserNameAvailable(login.Data);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        #endregion
    }

    
}
