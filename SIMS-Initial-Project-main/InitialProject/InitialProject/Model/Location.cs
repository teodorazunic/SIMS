using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Location
    {
        public string City { get; set; }

        public string State { get; set; }

        public string Adress { get; set; }
        
        public Location() { }

        public Location(string city, string state, string adress)
        {
            City = city;
            State = state;
            Adress = adress;
        }

    }
}
