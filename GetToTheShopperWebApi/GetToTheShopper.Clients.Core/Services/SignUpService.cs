using GetToTheShopper.Clients.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.Services
{
    public class SignUpService
    {
        public bool SignUp(RegisterDTO RegisterData)
        {
            return WebClient.ReadPostAsync<RegisterDTO>("Account/Register", RegisterData).Result;
        }
    }
}
