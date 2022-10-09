using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Models.DbModels;

namespace Farm.Models.ResponseModels
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; } //?
        public string SecondName { get; set; } //?

        public string Token { get; set; }

        public AuthResponse(User user, string token)
        {
            Id = user.Id;
            Login = user.Login;
            Role = user.Role;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            Token = token;
        }
    }
}
