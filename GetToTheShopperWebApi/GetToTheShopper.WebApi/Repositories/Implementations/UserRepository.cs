using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace GetToTheShopper.WebApi.Repositories.Implementations
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public GetToTheShopperContext SContext { get => Context as GetToTheShopperContext; }


        public string GetRoleId(string roleName)
        {
            return SContext.Roles.Where(r => r.Name == roleName).SingleOrDefault().Id;
        }
        public IEnumerable<ApplicationUser> GetAllClients()
        {
            return SContext.Users.Where(u =>
                SContext.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == GetRoleId("Client"))
                ).ToList();
        }
    }
}
