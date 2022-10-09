using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Models;
using Farm.Models.DbModels;
using Farm.Models.RequestModels;
using Farm.Models.ResponseModels;

namespace Farm.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class UserAccountController : Controller
    {
        ApplicationContext appCtx;
        public UserAccountController(ApplicationContext ctx)
        {
            appCtx = ctx;
        }

        [HttpPost("editProfile")]
        public IActionResult EditProfile([FromBody] UserModel usr)
        {
            User aa = appCtx.Users.FirstOrDefault(s => s.Login == usr.Login);
            if (aa != null)
            {
                aa.FirstName = usr.FirstName == null ? aa.FirstName : usr.FirstName;
                aa.SecondName = usr.SecondName == null ? aa.SecondName : usr.SecondName;
                aa.Password = usr.Password == null ? aa.Password : usr.Password;

                appCtx.Entry(aa).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appCtx.SaveChanges();
                return Ok();
            }
            return BadRequest(new { errorText = "Invalid User login" });

        }


    }
}
