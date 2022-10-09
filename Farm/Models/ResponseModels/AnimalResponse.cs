using Farm.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.ResponseModels
{
    public class AnimalResponse
    {
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        public bool Injected { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
    }
}
