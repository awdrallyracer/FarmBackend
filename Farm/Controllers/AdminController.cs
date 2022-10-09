
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
    [ApiController]
    //[Authorize]  
    public class AdminController : Controller
    {
        ApplicationContext appCtx;
        public AdminController(ApplicationContext ctx)
        {
            appCtx = ctx;
        }

        [HttpPost("addAnimal")]
        public IActionResult AddAnimal([FromBody] AnimalModel am)
        {
            Barn st = appCtx.Barns.FirstOrDefault(s => s.Id == am.BarnId);
            User user = (User)HttpContext.Items["User"];
            if (st != null)
            {
                Animal an = new Animal
                {
                    BarnId = am.BarnId,
                    
                    Name = am.Name,
                    Age =am.Age,
                    Weight = am.Weight,
                    Injection = am.Injection

                };

              
                appCtx.Animals.Add(an);
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid BarnId" });
        }

        [HttpPost("editAnimal")]
        public IActionResult EditAnimal([FromBody]AnimalModel anm)
        {
            Animal aa = appCtx.Animals.FirstOrDefault(s => s.Id == anm.Id);
            Injection inj = appCtx.Injections.FirstOrDefault(s => s.Id == anm.Id);
            if (aa != null)
            {
                aa.Name = anm.Name == null ? aa.Name : anm.Name;
                aa.Age = anm.Age == null ? aa.Age : anm.Age;
                aa.Weight = anm.Weight == null ? aa.Weight : anm.Weight;
                aa.Injection = anm.Injection == null ? aa.Injection : anm.Injection;
                aa.BarnId = anm.BarnId == null ? aa.BarnId : anm.BarnId;

                if(anm.Injection == true)
                {
                    Injection newInj = new Injection
                    {
                        InjectionTime = DateTime.Now,
                        Name = "Vaccine",
                        AnimalId = anm.Id,
                        
                    };
                    appCtx.Injections.Add(newInj);
                }

              

                appCtx.Entry(aa).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appCtx.SaveChanges();
                return Ok();
            }
            return BadRequest(new { errorText = "Invalid AnimalId" });

        }

        [HttpDelete("deleteAnimal/{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            Animal a = appCtx.Animals.FirstOrDefault(a => a.Id == id);
            if (a != null)
            {
                appCtx.Animals.Remove(a);
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid AnimalId" });

        }





        [HttpPost("addFood")]
        public IActionResult AddFood([FromBody] FoodModel ffm)
        {
            Animal a = appCtx.Animals.FirstOrDefault(a => a.Id == ffm.AnimalId);
            List<Care> c = appCtx.Cares.ToList();
            Food ss = new Food
            {
               Name = ffm.Name,
               Quantity = ffm.Quantity,
               Type = ffm.Type,
                AnimalId = ffm.AnimalId,
                Animal = a

            };

            appCtx.Foods.Add(ss);
            appCtx.SaveChanges();
            return Ok();
        }


        [HttpGet("getFood")] 
        public IActionResult GetFood(int id)
        {
            List<Food> result = appCtx.Foods.ToList();
            List<FoodResponse> final = new List<FoodResponse>();
            for (int i = 0; i < result.Count; i++)
            {
                    final.Add(new FoodResponse()
                    {
                        Id = result[i].Id,
                        Name = result[i].Name,
                        Type = result[i].Type,
                        Quantity = result[i].Quantity
                    });
            }
            return Json(final);
        }



        [HttpPost("editFood")]
        public IActionResult EditFood([FromBody]FoodModel ffd)
        {
            Food ff = appCtx.Foods.FirstOrDefault(s => s.Id == ffd.Id);
            Animal a = appCtx.Animals.FirstOrDefault(a => a.Id == ffd.AnimalId);
            if (ff != null)
            {
                ff.Name = ffd.Name == null ? ff.Name : ffd.Name;
                ff.Type = ffd.Type == null ? ff.Type : ffd.Type;
                ff.Quantity = ffd.Quantity == null ? ff.Quantity : ffd.Quantity;

                ff.AnimalId = ffd.AnimalId == null ? ff.AnimalId : ffd.AnimalId;

                appCtx.Entry(ff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appCtx.SaveChanges();
                return Ok();
            }
            return BadRequest(new { errorText = "Invalid FoodId" });

        }
        [HttpDelete("deleteFood/{id}")]
        public IActionResult DeleteFood(int id)
        {
            Food ss = appCtx.Foods.FirstOrDefault(s => s.Id == id);
            if (ss != null)
            {
                appCtx.Foods.Remove(ss);
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid FoodId" });

        }
        [HttpPost("addBarn")]
        public IActionResult AddBarn([FromBody] BarnModel brn)
        {
            Barn bn = new Barn
            {
                Name = brn.Name,
                Conditions = brn.Conditions
            };
            appCtx.Barns.Add(bn);
            appCtx.SaveChanges();
            return Ok();
        }

        [HttpPost("editBarn")]
        public IActionResult EditBarn([FromBody] BarnModel brn)
        {
            Barn ff = appCtx.Barns.FirstOrDefault(s => s.Id == brn.Id);
            if (ff != null)
            {
                ff.Name = brn.Name == null ? ff.Name : brn.Name;
                ff.Conditions = brn.Conditions == null ? ff.Conditions : brn.Conditions;

                appCtx.Entry(ff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appCtx.SaveChanges();
                return Ok();
            }
            return BadRequest(new { errorText = "Invalid BarnId" });

        }

        [HttpDelete("deleteBarn/{id}")]
        public IActionResult DeleteBarn(int id)
        {
            Barn bn = appCtx.Barns.FirstOrDefault(st => st.Id == id);
            if (bn != null)
            {
                appCtx.Barns.Remove(bn);
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid BarnId" });

        }


        [HttpGet("getInjectedAnimals")] //get method
        public IActionResult GetInjected()
        {
            List<Animal> res = appCtx.Animals.ToList();
            List<Animal> final = new List<Animal>();
            for (int i = 0; i < res.Count; i++)
            {
                if (res[i].Injection)
                {
                    final.Add(new Animal()
                    {
                        Id = res[i].Id,
                        Name = res[i].Name,
                        Age = res[i].Age,
                        Weight = res[i].Weight,
                        Injection = res[i].Injection
                    });
                } 
            }
            return Json(final);
        }


        [HttpGet("getAnimals")]
        public IActionResult GetAnimals()
        {
            List<Animal> res = appCtx.Animals.ToList();
            List<AnimalResponse> fin = new List<AnimalResponse>();
            for (int i = 0; i < res.Count; i++)
            {
                fin.Add(new AnimalResponse()
                {
                    AnimalId = res[i].Id,
                    AnimalName = res[i].Name,
                    Age = res[i].Age,
                    Weight = res[i].Weight,
                    Injected = res[i].Injection
                });
            }
            return Json(fin);
        }

        [HttpGet("getWorkers")]
        public IActionResult GetWorkers()
        {
            List<User> res = appCtx.Users.ToList();
            List<UserAccountResponse> fin = new List<UserAccountResponse>();
            for (int i = 0; i < res.Count; i++)
            {
                fin.Add(new UserAccountResponse()
                {
                    Id = res[i].Id,
                    Login = res[i].Login,
                    FirstName = res[i].FirstName,
                    SecondName = res[i].SecondName,
                    Role = res[i].Role
                });
            }
            return Json(fin);
        }


        [HttpGet("getInjections")]
        public IActionResult GetInjections()
        {
            List<Injection> res = appCtx.Injections.ToList();
           
            return Json(res);
        }



        [HttpGet("getBarns")]
          public IActionResult GetBarns()
          {
              List<Barn> res = appCtx.Barns.ToList();
              List<BarnResponse> fin = new List<BarnResponse>();
              for (int i = 0; i < res.Count; i++)
              {
                  fin.Add(new BarnResponse()
                  {
                      BarnId = res[i].Id,
                      BarnName = res[i].Name,
                      Conditions = res[i].Conditions,
                      Animals = GetAnimalResponse(res[i].Id)

                  });
              }
              return Json(fin);

          }


        private List<AnimalResponse> GetAnimalResponse(int barnId)
        {
            List<Animal> animals = appCtx.Animals.Where(b => b.BarnId == barnId).ToList();
            List<AnimalResponse> res = new List<AnimalResponse>();
            for(int i = 0; i < animals.Count; i++)
            {
                res.Add(new AnimalResponse()
                {
                    AnimalName = animals[i].Name,
                    AnimalId = animals[i].Id,
                    Injected = animals[i].Injection,
                    Age = animals[i].Age,
                    Weight = animals[i].Weight
                });
            }
            return res;
        }


    }
}
