using System;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Serializer;

using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.Models
{

    public class Renovation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation = new Accommodation();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }



        public Renovation() { }

        public Renovation(int id , Accommodation accommodation, DateTime startDate, DateTime endDate)

        {
            Id = id;
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;

        }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Accommodation.Id.ToString(), Accommodation.Name, StartDate.ToString(), EndDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation.Id = Convert.ToInt32(values[1]);
            Accommodation.Name = values[2];
            StartDate = Convert.ToDateTime(values[3]);
            EndDate = Convert.ToDateTime(values[4]);
        }  

    }
}
