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
    public class GuestOnTour : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }

        public string GuestName { get; set; }

        public int NumberOfGuests { get; set; }

        public KeyPoint StartingKeyPoint { get; set; }

        public bool Status { get; set; }

        public int Age { get; set; }


        public GuestOnTour(int id, int guestId, string guestName, int numberOfGuests, KeyPoint startingKeyPoint, bool status)
        {
            Id = id;
            GuestId = guestId;
            GuestName = guestName;
            NumberOfGuests = numberOfGuests;
            StartingKeyPoint = startingKeyPoint;
            Status = false;
         }
        public GuestOnTour() { }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), GuestName,NumberOfGuests.ToString(), StartingKeyPoint.Id.ToString(), StartingKeyPoint.Name, StartingKeyPoint.Tour.Id.ToString(), StartingKeyPoint.Status, Status.ToString() , Age.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]); 
            GuestId = Convert.ToInt32(values[1]);
            GuestName = values[2];
            NumberOfGuests = Convert.ToInt32(values[3]);
            StartingKeyPoint = new KeyPoint() { Id = Convert.ToInt32(values[4]), Name = values[5], Tour = new Tour() { Id = Convert.ToInt32(values[6]) }, Status = values[7] };
            Status = Convert.ToBoolean(values[8]);
            Age = Convert.ToInt32(values[9]);
        }
    }
}
