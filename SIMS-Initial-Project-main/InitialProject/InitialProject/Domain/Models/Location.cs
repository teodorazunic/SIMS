using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class Location
    {
        public string City { get; set; }

        public string Country { get; set; }


        public Location() { }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }

    }
}
