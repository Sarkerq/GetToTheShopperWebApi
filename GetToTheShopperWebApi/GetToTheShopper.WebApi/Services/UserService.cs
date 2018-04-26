using GetToTheShopper.WebApi.Exceptions;
using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace GetToTheShopper.WebApi.Services
{
    public class UserService
    {
        GetToTheShopperContext context;
        public UserService(GetToTheShopperContext context)
        {
            this.context = context;
        }

        public ApplicationUser GetUserById(string id)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                var User = unitOfWork.Users.GetAll().SingleOrDefault(u=> u.Id == id);
                if (User == null)
                    throw new NonExistingRecordException("User", "id");
                return User;
            }
        }

        public List<ApplicationUser> GetClientsList()
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return unitOfWork.Users.GetAllClients().ToList();
            }
        }

        public List<ApplicationUser> GetClientsListExcept(string userName)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return GetClientsList().Where(u => u.UserName != userName).ToList();
            }
        }

        public bool CheckIfUserNameAvailable(string login)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                return !unitOfWork.Users.Any(u => u.UserName == login);
            }
        }
    }
}
