using InitialProject.Serializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Domain.Models
{
    internal class GuestOnTour : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }

        public string GuestName { get; set; }

        public int NumberOfGuests { get; set; }

        public KeyPoint StartingKeyPoint { get; set; }


        

        public GuestOnTour(int id, int guestId, string guestName, int numberOfGuests, KeyPoint startingKeyPoint)
        {
            Id = id;
            GuestId = guestId;
            GuestName = guestName;
            NumberOfGuests = numberOfGuests;
            StartingKeyPoint = startingKeyPoint;
        }
        public GuestOnTour() { }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), GuestName,NumberOfGuests.ToString(), StartingKeyPoint.Id.ToString(), StartingKeyPoint.Name, StartingKeyPoint.TourId.ToString(), StartingKeyPoint.Status };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]); 
            GuestId = Convert.ToInt32(values[1]);
            GuestName = values[2];
            NumberOfGuests = Convert.ToInt32(values[3]);
            StartingKeyPoint = new KeyPoint() { Id = Convert.ToInt32(values[4]), Name = values[5], TourId = Convert.ToInt32(values[6]), Status = values[7] };

        }
    }
}
