using Farm.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.RequestModels
{
    public class CareModel
    {
        public int AnimalId { get; set; }
        public int? UserId { get; set; }
        public int? BarnId { get; set; }
        public int? FoodId { get; set; }




        /*public int Id { get; set; }
        public string AnimalName { get; set; }
        public string WorkerName { get; set; }
        public string FoodName { get; set; }
        public bool isInjection { get; set; }
        public DateTime? InjectionTime { get; set; }*/

    }
}
