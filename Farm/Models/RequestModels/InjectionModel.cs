using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.RequestModels
{
    public class InjectionModel
    {
        public string Name { get; set; }
        public DateTime? InjectionTime { get; set; }

        public int AnimalId { get; set; }
    }
}
