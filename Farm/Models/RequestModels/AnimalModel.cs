using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.RequestModels
{
    public class AnimalModel
    {
        public int UserId { get; set; }//?
        public int BarnId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public bool Injection { get; set; }
    }
}
