using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.DbModels
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public bool Injection { get; set; }

        

        public User User { get; set; }
        public int? UserId { get; set; }
        public Barn Barn { get; set; }
        public int? BarnId { get; set; }

        public List<Injection> Injections { get; set; }
        public List<Food> Foods { get; set; }
        public List<Care> Cares { get; set; }
    }
}
