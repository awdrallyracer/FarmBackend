using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Helpers;
using Farm.Models;
using Farm.Models.DbModels;
using Farm.Models.ResponseModels;
using Farm.Models.RequestModels;

namespace Farm.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    //[Authorize]  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public class CareController : Controller
    {
        ApplicationContext appCtx;
        public CareController(ApplicationContext ctx)
        {
            appCtx = ctx;
        }

        [HttpPost("takeCare")]
        public IActionResult AddCare([FromBody] CareModel cm)
        {
            Care care = appCtx.Cares.FirstOrDefault(s => s.AnimalId == cm.AnimalId);

            if (care == null)
            {
                Animal animal = appCtx.Animals.FirstOrDefault(s => s.Id == cm.AnimalId);
                Barn barn = appCtx.Barns.FirstOrDefault(s => s.Id == cm.BarnId);
                Food food = appCtx.Foods.FirstOrDefault(s => s.AnimalId == cm.AnimalId);
                User user = (User)HttpContext.Items["User"];

                Care fullCare = new Care
                {
                    AnimalId = cm.AnimalId,
                    AnimalName = animal.Name,
                    UserId = user.Id,
                    WorkerName = user.Login,
                    FoodName = food.Name,

                    BarnId = animal.BarnId,
                    FoodId = food.Id
                };
                appCtx.Cares.Add(fullCare);
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid AnimalId" });
        }




        [HttpGet("getCares")]
        public IActionResult GetCares()
        {
            List<Care> result = appCtx.Cares.ToList();
            List<CareResponse> final = new List<CareResponse>();
            for (int i = 0; i < result.Count; i++)
            {
                final.Add(new CareResponse()
                {
                    Id = result[i].Id,
                    AnimalName = result[i].AnimalName,
                    WorkerName = result[i].WorkerName,
                    FoodName = result[i].FoodName,
                    isInjection = result[i].isInjection,
                    InjectionTime = result[i].InjectionTime
                
                });
            }
            return Json(final);
        }

      


    }
}
