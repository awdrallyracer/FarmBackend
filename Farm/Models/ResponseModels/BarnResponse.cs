using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Models.ResponseModels
{
    public class BarnResponse
    {
        public List<AnimalResponse> Animals { get; set; }
        public string AnimalName { get; set; }

        public int BarnId { get; set; }
        public string BarnName { get; set; }
        public string Conditions { get; set; }
    }
}
