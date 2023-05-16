using System;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Domain.Models
{
    public class KeyPointsOnTour : ISerializable
    {
        public int TourId { get; set; }

        public string KeyPoints { get; set; }

        public KeyPointsOnTour(int tourId, string keyPoints)
        {
            TourId = tourId;
            KeyPoints = keyPoints;
        }

        public KeyPointsOnTour() { }

        public string[] ToCSV()
        {
            string[] csvValues = { TourId.ToString(), KeyPoints};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            TourId = Convert.ToInt32(values[0]);
            KeyPoints = values[1];
        }
    }
}
