using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.DbModels
{
    public class Barn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Conditions { get; set; }

        public List<Animal> Animals { get; set; }
        public List<Care> Cares { get; set; }

    }
}
