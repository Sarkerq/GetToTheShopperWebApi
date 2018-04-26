using GetToTheShopper.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace GetToTheShopper.WebApi.DTO.Assemblers
{
    public class UserAssembler
    {
        public UserDTO GetDTO(ApplicationUser User)
        {
            return new UserDTO()
            {
                Username = User.UserName,
                Id = User.Id
            };
        }
    }
}
