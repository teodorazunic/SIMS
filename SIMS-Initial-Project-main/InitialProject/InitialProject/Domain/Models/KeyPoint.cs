using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class KeyPoint :ISerializable
    {
        
        public string Name { get; set; }

        public int Id { get; set; }

        //public int TourId { get; set; }

        public Tour Tour = new Tour();

        public string Status { get; set; }
        
        public KeyPoint() { }


        public KeyPoint(string name, int id, Tour tourId)
        {
            Name = name;
            Id = id;
            Tour = tourId;
            Status = "Inactive";
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Tour.Id.ToString(), Status};
            return csvValues;
        }

        

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Tour = new Tour() { Id = Convert.ToInt32(values[2]) };
            Status = values[3];
        }
    }
}
