using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class KeyPoint
    {
        public Location Location { get; set; }

        public string Atrraction { get; set; }

        public int AtrractionId { get; set; }
        
        public KeyPoint() { }

        public KeyPoint(Location location, string atrraction, int atrractionId)
        {
            Location = location;
            Atrraction = atrraction;
            AtrractionId = atrractionId;
        }
    }
}
