using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.ResponseModels
{
    public class CareResponse
    {
        public int Id { get; set; }
        public string AnimalName { get; set; }
        public string WorkerName { get; set; }
        public string FoodName { get; set; }
        public bool isInjection { get; set; }
        public DateTime? InjectionTime { get; set; }
    }
}
