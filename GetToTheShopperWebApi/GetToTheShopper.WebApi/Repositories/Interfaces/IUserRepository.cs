using GetToTheShopper.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace GetToTheShopper.WebApi.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        string GetRoleId(string roleName);
        IEnumerable<ApplicationUser> GetAllClients();
    }
}
