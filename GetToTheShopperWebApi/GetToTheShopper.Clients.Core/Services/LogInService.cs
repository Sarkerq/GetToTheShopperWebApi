using GetToTheShopper.Clients.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.Services
{
    public class LogInService
    {
        public bool LogIn(LoginDTO Login)
        {
            return WebClient.ReadPostAsync<LoginDTO>("Account/Login", Login).Result;
        }
    }
}
