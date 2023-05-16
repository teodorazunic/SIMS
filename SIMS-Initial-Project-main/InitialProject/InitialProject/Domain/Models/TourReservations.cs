using System;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class TourReservations : ISerializable
    {
        public int TourReservationId { get; set; }
        
        //public int TourId { get; set; }

        public Tour Tour = new Tour();

        //public int GuestId { get; set; }

        public User GuestId = new User();

        public int NumberOfGuests { get; set; }

        public bool UsedVoucher { get; set; }
        
        public string Status { get; set; }

        public TourReservations() { }

        public TourReservations(int tourReservationId, Tour tour, User guestId, int numberOfGuests, bool usedVoucher, string status)
        {
            TourReservationId = tourReservationId;
            Tour = tour;
            GuestId = guestId;
            NumberOfGuests = numberOfGuests;
            UsedVoucher = usedVoucher;
            Status = status;
        }

        public void FromCSV(string[] values)
        {
            TourReservationId = Convert.ToInt32(values[0]);
            Tour = new Tour() { Id = Convert.ToInt32(values[1]) };
            GuestId =new User() { Id = Convert.ToInt32(values[2]) };
            NumberOfGuests = Convert.ToInt32(values[3]);
            UsedVoucher = Convert.ToBoolean(values[4]);
            Status = values[5];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { TourReservationId.ToString(),Tour.Id.ToString(), GuestId.Id.ToString(), NumberOfGuests.ToString(), UsedVoucher.ToString(), Status };
            return csvValues;
        }
    }
}
