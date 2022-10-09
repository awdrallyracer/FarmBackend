using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.RequestModels
{
    public class BarnModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Conditions { get; set; }
    }
}
