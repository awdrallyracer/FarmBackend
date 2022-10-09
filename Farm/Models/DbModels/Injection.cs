using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.DbModels
{
    public class Injection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? InjectionTime { get; set; }

        public Animal Animal { get; set; }
        public int? AnimalId { get; set; }

        public List<Care> Cares { get; set; }
    }
}
