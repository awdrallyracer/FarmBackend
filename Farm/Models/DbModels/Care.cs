using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.DbModels
{
    public class Care
    {
        public int Id { get; set; }
        public string AnimalName { get; set; }
        public string WorkerName { get; set; }
        public string FoodName { get; set; }
        public bool isInjection { get; set; }
        public DateTime? InjectionTime {get;set;}


        public Injection Injection { get; set; }
        public int? InjectionId { get; set; }
        public Animal Animal { get; set; }
        public int? AnimalId { get; set; }
        public Barn Barn { get; set; }
        public int? BarnId { get; set; }
        public Food Food { get; set; }
        public int? FoodId { get; set; }
        public User User { get; set; }
        public int? UserId {get;set;}
    }
}
