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

        public int TourId { get; set; }

        public string Status { get; set; }
        
        public KeyPoint() { }


        public KeyPoint(string name, int id, int tourId)
        {
            Name = name;
            Id = id;
            TourId = tourId;
            Status = "Inactive";
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, TourId.ToString(), Status};
            return csvValues;
        }

        

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            TourId = Convert.ToInt32(values[2]);
            Status = values[3];
        }
    }
}
