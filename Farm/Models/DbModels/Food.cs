using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.DbModels
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }

        public Animal Animal { get; set; }
        public int? AnimalId { get; set; }

        public List<Care> Cares { get; set; }

    }
}
