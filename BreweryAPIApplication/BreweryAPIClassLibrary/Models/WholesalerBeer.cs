using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreweryAPIClassLibrary.Models
{
    public class WholesalerBeer
    {
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int Stock { get; set; }
    }
}
