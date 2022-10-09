using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Farm.Models;
using Farm.Models.DbModels;
using Farm.Models.ResponseModels;
using Farm.Models.RequestModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace Farm.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]

    public class AuthController : Controller
    {
        ApplicationContext appCtx;
        public AuthController(ApplicationContext context)
        {
            appCtx = context;
        }

        //-------------------------------------------------------------------------------------------

        public User AddUser(string login, string password, string firstName, string secondName)
        {
            if (appCtx.Users.FirstOrDefault(u => u.Login == login) == null)
            {
                //Validation
                User user = new User { Login = login, Password = password, Role = "user", FirstName = firstName, SecondName = secondName};

                appCtx.Users.Add(user);
                appCtx.SaveChanges();
                var u = appCtx.Users.ToList().Last();
                //u.Statistics = new Statistics { UserId = u.Id };
                appCtx.Entry(u).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appCtx.SaveChanges();

                return user;
            }
            return null;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel us)
        {
            User u = AddUser(us.Login, us.Password, us.FirstName, us.SecondName);
            if (u != null)
            {

                return Json(Authenticate(new UserModel { Login = u.Login, Password = u.Password, FirstName = u.FirstName, SecondName = u.SecondName }));
            }
            return BadRequest(new { errormesage = "This login is already used" });
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost("authenticate")]
        public IActionResult Authentication([FromBody] UserModel usr)
        {
            var a = Authenticate(usr);
            if (a == null)
            {
                return new UnauthorizedResult();
            }
            return Json(a);
        }


        public AuthResponse Authenticate(UserModel usr)
        {
            User user = appCtx.Users.FirstOrDefault(x => x.Login == usr.Login && x.Password == usr.Password);
            var identity = GetIdentity(user);
            if (identity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthResponse(user, encodedJwt);
        }

        private ClaimsIdentity GetIdentity(User u)
        {

            if (u != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, u.Login),
                    new Claim("id", u.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, u.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
