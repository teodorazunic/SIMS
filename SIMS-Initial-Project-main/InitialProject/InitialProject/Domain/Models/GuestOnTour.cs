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

        public int Age { get; set; }

        public string Voucher { get; set; }



        public GuestOnTour(int id, int guestId, string guestName,int age, int numberOfGuests, KeyPoint startingKeyPoint, string voucher)
        {
            Id = id;
            GuestId = guestId;
            GuestName = guestName;
            Age = age;
            NumberOfGuests = numberOfGuests;
            StartingKeyPoint = startingKeyPoint;
            Voucher = voucher;
        }
        public GuestOnTour() { }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), GuestName,Age.ToString(),NumberOfGuests.ToString(), StartingKeyPoint.Id.ToString(), StartingKeyPoint.Name, StartingKeyPoint.TourId.ToString(), StartingKeyPoint.Status, Voucher};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]); 
            GuestId = Convert.ToInt32(values[1]);
            GuestName = values[2];
            Age = Convert.ToInt32(values[3]);
            NumberOfGuests = Convert.ToInt32(values[4]);
            StartingKeyPoint = new KeyPoint() { Id = Convert.ToInt32(values[5]), Name = values[6], TourId = Convert.ToInt32(values[7]), Status = values[8] };
            Voucher  = values[9];
        }
    }
}
