using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto.Model
{
    public class Locations
    {
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Locations))
            {
                return false;
            }
            Locations loc = (Locations)obj;
            return Name.Equals(loc.Name) && Latitude.Equals(loc.Latitude) && Longitude.Equals(loc.Longitude);
        }
    }
}
